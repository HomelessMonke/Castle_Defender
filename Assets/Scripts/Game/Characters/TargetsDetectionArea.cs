using System;
using System.Collections.Generic;
using System.Linq;
using Game.Characters.Units;
using UnityEngine;
using Utilities;
using Utilities.Extensions;
using Random = UnityEngine.Random;

namespace Game.Characters
{
    public class TargetsDetectionArea : MonoBehaviour
    {
        [SerializeField]
        LayerMask layerMask;
        
        [SerializeField]
        float updatePerSeconds = 1.5f;
        
        int maxUnitsToTarget = 10;
        
        List<IDamageable> targetsInArea;

        CustomTimer timer;
        float timeFromUpdate;
        
        public event Action TargetsUpdated;

        public void Init(int capacity)
        {
            targetsInArea = new List<IDamageable>(capacity);
            timer = new CustomTimer(updatePerSeconds);
            timer.TimerEnd += OnTimerEnd;
        }
        
        /// <param name="targetRange"> Максимальное кол-во юнитов среди которых будет выбираться таргет</param>
        public void SetMaxTargetsRange(int targetRange)
        {
            maxUnitsToTarget = targetRange;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            timer.Stop();
            targetsInArea.Clear();
            gameObject.SetActive(false);
        }
        

        void Update()
        {
            timer.Tick(Time.deltaTime);
        }
        
        public (IDamageable, bool) GetClosestTarget(Vector2 comparePos, float range)
        {
            if (targetsInArea.Count == 0)
                return (null, false);

            var closestTarget = targetsInArea[0];
            bool inRange = false;
            if (targetsInArea.Count > 1)
            {
                var sqrRange = range * range;
                var targetSqrDistance = ((Vector2)closestTarget.Transform.position - comparePos).sqrMagnitude;
                var lenght = targetsInArea.Count >= maxUnitsToTarget? maxUnitsToTarget : targetsInArea.Count;
                for (int i = 1; i < lenght; i++)
                {
                    var targetInArea = targetsInArea[i];
                    Vector2 distance = (Vector2)targetInArea.Transform.position - comparePos;
                    float sqrDistance = distance.sqrMagnitude;
                    if (sqrDistance < targetSqrDistance)
                    {
                        closestTarget = targetInArea;
                        targetSqrDistance = sqrDistance;
                        if (sqrDistance < sqrRange)
                        {
                            inRange = true;
                        }
                    }
                }
            }
            return (closestTarget, inRange);
        }
        
        public (IDamageable, bool) GetRandomTargetInRange(Vector2 comparePos, float range)
        {
            if (targetsInArea.Count == 0)
                return (null,false);
            
            if (targetsInArea.Count > 1)
            {
                var targetsInRange = new List<IDamageable>();
                var sqrRange = range * range;

                IDamageable closestTarget = null;
                var minDistance = Mathf.Infinity;
                var lenght = targetsInArea.Count >= maxUnitsToTarget? maxUnitsToTarget : targetsInArea.Count;
                for (int i = 1; i < lenght; i++)
                {
                    var target = targetsInArea[i];
                    Vector2 distance = (Vector2)target.Transform.position - comparePos;
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

        void OnTimerEnd()
        {
            SortTargetsByAxisX();
            timer.Restart();
            
            if(targetsInArea.Count > 0)
                TargetsUpdated?.Invoke();
        }
        
        void SortTargetsByAxisX()
        {
            if (targetsInArea == null || targetsInArea.Count < 2)
                return;
            
            targetsInArea = targetsInArea.OrderBy(x=> x.Transform.position.x).ToList();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;
            if (layerMask.Contains(otherObj.layer))
            {
                var character = otherObj.GetComponent<IDamageable>();
                if (character != null && character.IsAlive)
                {
                    targetsInArea.Add(character);
                    if(targetsInArea.Count > 1 && !timer.IsRunning)
                        timer.Start();
                }
            }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            var otherObj = other.gameObject;
            if (!layerMask.Contains(otherObj.layer))
                return;
            
            var character = otherObj.GetComponent<IDamageable>();
            targetsInArea.Remove(character);
            
            if(targetsInArea.Count <= 1)
                timer.Stop();
        }
    }
}