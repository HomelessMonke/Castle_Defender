using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities.Extensions;

namespace Game.Characters
{
    public class CharacterFieldOfView : MonoBehaviour
    {
        [SerializeField]
        LayerMask layerMask;
        
        [SerializeField]
        Collider2D collider2D;

        [Header("Обновление fov раз в N кадров")]
        [SerializeField]
        int updateFrameRate = 2;
        
        [SerializeField]
        Character character;

        int frameCounter;
        
        Transform currentTarget;
        List<Transform> triggeredObjects = new List<Transform>();
        
        public Transform CurrentTarget => currentTarget;
        
        public event UnityAction<Transform> TargetChanged;

        public void Init()
        {
            TargetChanged = null;
            TargetChanged += (target) =>
            {
                character.stateLog.Append($"<color=yellow>{nameof(TargetChanged)} event {target}</color>");
                character.stateLog.Append(Environment.NewLine);
                character.stateLog.Append($"<color=yellow>triggeredObjects.Count {triggeredObjects.Count}</color>");
                character.stateLog.Append(Environment.NewLine);
            };
            currentTarget = null;
            triggeredObjects.Clear();
        }

        public void UpdateViewTarget()
        {
            // frameCounter++;
            // if (frameCounter == updateFrameRate)
            // {
            //     frameCounter = 0;
            //     
            // }
            TryUpdateTarget();
        }

        public void TryUpdateTarget()
        {
            var isTargetChanged = false;
            
            if(currentTarget && !currentTarget.gameObject.activeSelf)
            {
                character.stateLog.Append($"{nameof(TryUpdateTarget)}1 {currentTarget.name} isn't Active");
                character.stateLog.Append(Environment.NewLine);
                character.stateLog.Append($"{nameof(TryUpdateTarget)}1 {triggeredObjects.Count}");
                character.stateLog.Append(Environment.NewLine);
                triggeredObjects.Remove(currentTarget);
                currentTarget = null;
                isTargetChanged = true;
            }
            
            for (int i = triggeredObjects.Count-1; i >=0 ; i--)
            {
                var triggeredObj = triggeredObjects[i];

                if (!triggeredObj.gameObject.activeSelf)
                {
                    character.stateLog.Append($"{nameof(TryUpdateTarget)}2  {triggeredObj.name} isn't Active");
                    character.stateLog.Append(Environment.NewLine);
                    character.stateLog.Append($"{nameof(TryUpdateTarget)}2 {triggeredObjects.Count}");
                    character.stateLog.Append(Environment.NewLine);
                    triggeredObjects.Remove(triggeredObj);
                    continue;
                }
                
                if (!currentTarget)
                {
                    currentTarget = triggeredObj;
                    isTargetChanged = true;
                    continue;
                }

                if (triggeredObjects.Count > 1)
                {
                    var sqrTargetDistance = (currentTarget.position - character.transform.position).sqrMagnitude;
                    var sqrTriggeredObjDistance = (triggeredObj.position - character.transform.position).sqrMagnitude;
                    if (sqrTriggeredObjDistance < sqrTargetDistance)
                    {
                        currentTarget = triggeredObj;
                        isTargetChanged = true;
                    }
                }
            }

            if (!currentTarget && isTargetChanged)
                ;
            
            if(isTargetChanged)
                TargetChanged?.Invoke(currentTarget);
            
            // Debug.Log($"{character.name} triggeredObjects Count: {triggeredObjects.Count}");
            // if(currentTarget)
            //     Debug.Log($"{character.name} currentTarget: {currentTarget.name}");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;
            if (layerMask.Contains(otherObj.layer))
            {
                if (!triggeredObjects.Contains(otherObj.transform))
                {
                    triggeredObjects.Add(otherObj.transform);
                    character.stateLog.Append($"<color=green>{nameof(OnTriggerEnter2D)} {otherObj.name}</color>");
                    character.stateLog.Append(Environment.NewLine);
                }
            }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            if(!character.IsAlive)
                return;
                
            var otherObj = other.gameObject;
            if (!layerMask.Contains(otherObj.layer))
                return;
            
            character.stateLog.Append($"<color=red>{nameof(OnTriggerExit2D)} {otherObj.name}</color>");
            character.stateLog.Append(Environment.NewLine);
            
            triggeredObjects.Remove(otherObj.transform);
            TryUpdateTarget();     
        }
    }
}