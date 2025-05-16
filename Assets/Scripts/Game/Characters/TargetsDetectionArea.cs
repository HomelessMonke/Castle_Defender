using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;
using Random = UnityEngine.Random;

namespace Game.Characters
{
    public class TargetsDetectionArea : MonoBehaviour
    {
        [SerializeField]
        LayerMask layerMask;

        List<Health> targetsInArea;
        
        public event Action TargetsChanged;

        public void Init(int capacity)
        {
            targetsInArea = new List<Health>(capacity);
        }

        public Health GetTargetByIndex(int index)
        {
            if (targetsInArea.Count == 0)
                return null;
                
            var targetIndex = index % targetsInArea.Count;
            return targetsInArea[targetIndex];
        }
        
        public (Health, bool) GetClosestTarget(Vector2 comparePos, float range)
        {
            if (targetsInArea.Count == 0)
                return (null, false);

            var closestTarget = targetsInArea[0];
            bool inRange = false;
            if (targetsInArea.Count > 1)
            {
                var sqrRange = range * range;
                var targetSqrDistance = ((Vector2)closestTarget.transform.position - comparePos).sqrMagnitude;
                for (int i = 1; i < targetsInArea.Count; i++)
                {
                    var targetInArea = targetsInArea[i];
                    Vector2 distance = (Vector2)targetInArea.transform.position - comparePos;
                    float sqrDistance = distance.sqrMagnitude;
                    if (sqrDistance < targetSqrDistance)
                    {
                        closestTarget = targetInArea;
                        targetSqrDistance = sqrDistance;
                        if (sqrDistance < range)
                        {
                            inRange = true;
                        }
                    }
                }
            }
            return (closestTarget, inRange);
        }
        
        public (Health, bool) GetRandomTargetInRange(Vector2 comparePos, float range)
        {
            if (targetsInArea.Count == 0)
                return (null,false);
            
            if (targetsInArea.Count > 1)
            {
                var targetsInRange = new List<Health>();
                var sqrRange = range * range;

                Health closestTarget = null;
                var minDistance = Mathf.Infinity;
                for (int i = 1; i < targetsInArea.Count; i++)
                {
                    var target = targetsInArea[i];
                    Vector2 distance = (Vector2)target.transform.position - comparePos;
                    float targetSqrDistance = distance.sqrMagnitude;
                    if (targetSqrDistance < sqrRange)
                    {
                        targetsInRange.Add(target);
                    }
                    else
                    {
                        if (targetSqrDistance < minDistance)
                        {
                            minDistance = targetSqrDistance;
                            closestTarget = target;
                        }
                    }
                }

                if (targetsInRange.Count > 0)
                {
                    var index = Random.Range(0, targetsInRange.Count);
                    return (targetsInRange[index], true);
                }
                   
                return (closestTarget, false);
            }
            
            return (targetsInArea[0], false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;
            if (layerMask.Contains(otherObj.layer))
            {
                var hpComponent = otherObj.GetComponent<Health>();
                if (hpComponent.IsAlive)
                {
                    targetsInArea.Add(hpComponent);
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
            targetsInArea.Remove(hpComponent);
            TargetsChanged?.Invoke();
        }
    }
}