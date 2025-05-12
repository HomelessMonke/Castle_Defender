using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;

namespace Game.Characters
{
    public class TargetsDetectionArea : MonoBehaviour
    {
        [SerializeField]
        LayerMask layerMask;

        List<Health> targets;
        
        public bool HaveTargets => targets.Count > 0;

        public event Action TargetsChanged;

        public void Init(int capacity)
        {
            targets = new List<Health>(capacity);
        }

        public void RegisterTargets(Health[] addTargets)
        {
            targets.AddRange(addTargets);
        }

        public Health GetTargetByIndex(int index)
        {
            if (targets.Count == 0)
                return null;
                
            var targetIndex = index % targets.Count;
            return targets[targetIndex];
        }
        
        public Health GetTargetByDistance(Vector2 comparePos)
        {
            if (targets.Count == 0)
                return null;

            var target = targets[0];
            if (targets.Count > 1)
            {
                var targetSqrDistance = ((Vector2)target.transform.position - comparePos).sqrMagnitude;
                for (int i = 1; i < targets.Count; i++)
                {
                    var obj = targets[i];
                    Vector2 direction = (Vector2)obj.transform.position - comparePos;
                    float objSqrDistance = direction.sqrMagnitude;
                    if (objSqrDistance < targetSqrDistance)
                    {
                        target = obj;
                        targetSqrDistance = objSqrDistance;
                    }
                }
            }
            return target;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;
            if (layerMask.Contains(otherObj.layer))
            {
                var hpComponent = otherObj.GetComponent<Health>();
                if (hpComponent.IsAlive)
                {
                    targets.Add(hpComponent);
                    Debug.Log($"{name} add new trigger {hpComponent.name}");
                    TargetsChanged?.Invoke();
                }
            }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            var otherObj = other.gameObject;
            if (!layerMask.Contains(otherObj.layer))
                return;
            
            var hpComponent = otherObj.GetComponent<Health>();
            targets.Remove(hpComponent);
            Debug.Log($"{name} remove new trigger {hpComponent.name}");
            TargetsChanged?.Invoke();
        }
    }
}