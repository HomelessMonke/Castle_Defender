using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;

namespace Game.Characters
{
    public class MultiTargetFieldOfView : MonoBehaviour
    {
        [SerializeField]
        LayerMask layerMask;
        
        List<Health> triggeredObjects = new (32);
        
        public bool HaveTargets => triggeredObjects.Count > 0;

        public event Action TargetsChanged;

        public Health GetTargetByIndex(int index)
        {
            if (triggeredObjects.Count == 0)
                return null;
                
            var targetIndex = index % triggeredObjects.Count;
            return triggeredObjects[targetIndex];
        }
        
        public Health GetTargetByDistance(Vector2 comparePos)
        {
            if (triggeredObjects.Count == 0)
                return null;

            var target = triggeredObjects[0];
            if (triggeredObjects.Count > 1)
            {
                var targetSqrDistance = ((Vector2)target.transform.position - comparePos).sqrMagnitude;
                for (int i = 1; i < triggeredObjects.Count; i++)
                {
                    var obj = triggeredObjects[i];
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
                    triggeredObjects.Add(hpComponent);
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
            triggeredObjects.Remove(hpComponent);
            TargetsChanged?.Invoke();
        }
    }
}