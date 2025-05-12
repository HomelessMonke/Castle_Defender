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
        
        Transform currentTarget;
        Transform characterTransform;
        List<Transform> triggeredObjects = new ();
        
        public Transform CurrentTarget => currentTarget;
        
        public event UnityAction<Transform> TargetChanged;

        public void Init(Transform characterTransform)
        {
            currentTarget = null;
            triggeredObjects.Clear();
            this.characterTransform = characterTransform;
        }

        public void Update()
        {
            TryUpdateTarget();
        }

        void TryUpdateTarget()
        {
            var isTargetChanged = false;
            
            if(currentTarget && !currentTarget.gameObject.activeSelf)
            {
                triggeredObjects.Remove(currentTarget);
                currentTarget = null;
                isTargetChanged = true;
            }
            
            for (int i = triggeredObjects.Count-1; i >=0 ; i--)
            {
                var triggeredObj = triggeredObjects[i];

                if (!triggeredObj.gameObject.activeSelf)
                {
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
                    var sqrTargetDistance = (currentTarget.position - characterTransform.position).sqrMagnitude;
                    var sqrTriggeredObjDistance = (triggeredObj.position - characterTransform.position).sqrMagnitude;
                    if (sqrTriggeredObjDistance < sqrTargetDistance)
                    {
                        currentTarget = triggeredObj;
                        isTargetChanged = true;
                    }
                }
            }
            
            if(isTargetChanged)
                TargetChanged?.Invoke(currentTarget);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;
            if (layerMask.Contains(otherObj.layer))
            {
                if (!triggeredObjects.Contains(otherObj.transform))
                {
                    triggeredObjects.Add(otherObj.transform);
                }
            }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            if(!gameObject.activeInHierarchy)
                return;
                
            var otherObj = other.gameObject;
            if (!layerMask.Contains(otherObj.layer))
                return;
            
            triggeredObjects.Remove(otherObj.transform);
            TryUpdateTarget();     
        }

        void OnDestroy()
        {
            TargetChanged = null;
        }
    }
}