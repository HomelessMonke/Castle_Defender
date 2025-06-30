using System.Collections.Generic;
using Game.Characters.Units;
using UnityEngine;
using UnityEngine.Events;
using Utilities.Extensions;

namespace Game.Characters
{
    public class CharacterFieldOfView : MonoBehaviour
    {
        [SerializeField]
        LayerMask layerMask;
        
        string currentTargetID;
        IDamageable currentTarget;
        List<IDamageable> triggeredCharacters = new ();
        
        public event UnityAction<IDamageable> TargetUpdated;

        public void Init()
        {
            ResetTarget();
            triggeredCharacters.Clear();
        }

        public void UpdateTarget()
        {
            if(currentTarget != null && (!currentTarget.IsAlive || currentTarget.Id != currentTargetID))
            {
                triggeredCharacters.Remove(currentTarget);
                ResetTarget();
            }

            var fovPos = transform.position;
            for (int i = triggeredCharacters.Count-1; i >=0 ; i--)
            {
                var triggeredObj = triggeredCharacters[i];

                if (!triggeredObj.IsAlive)
                {
                    triggeredCharacters.Remove(triggeredObj);
                    continue;
                }
                
                if (currentTarget == null)
                {
                    SetTarget(triggeredObj);
                    continue;
                }

                if (triggeredCharacters.Count > 1)
                {
                    var sqrCurrentTargetDistance = (currentTarget.Transform.position - fovPos).sqrMagnitude;
                    var sqrTriggeredObjDistance = (triggeredObj.Transform.position - fovPos).sqrMagnitude;
                    if (sqrTriggeredObjDistance < sqrCurrentTargetDistance)
                    {
                        SetTarget(triggeredObj);
                    }
                }
            }
            
            TargetUpdated?.Invoke(currentTarget);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;
            if (layerMask.Contains(otherObj.layer))
            {
                var character = otherObj.GetComponent<IDamageable>();
                if (!triggeredCharacters.Contains(character))
                {
                    triggeredCharacters.Add(character);
                    UpdateTarget();
                }
            }
        }

        void ResetTarget()
        {
            currentTarget = null;
            currentTargetID = string.Empty;
        }

        void SetTarget(IDamageable target)
        {
            currentTarget = target;
            currentTargetID = target.Id;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if(!gameObject.activeInHierarchy)
                return;
                
            var otherObj = other.gameObject;
            if (!layerMask.Contains(otherObj.layer))
                return;
            
            var character = otherObj.GetComponent<IDamageable>();
            triggeredCharacters.Remove(character);
            UpdateTarget();     
        }

        void OnDestroy()
        {
            TargetUpdated = null;
        }
    }
}