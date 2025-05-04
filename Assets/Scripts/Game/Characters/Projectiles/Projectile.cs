using System;
using UnityEngine;

namespace Game.Characters.Projectiles
{
    public abstract class Projectile: MonoBehaviour
    {
        protected HealthComponent target;
        protected Vector2 targetPos;
        
        public event Action Hit;

        public abstract void Launch(HealthComponent target, ProjectileAnimationData animationData, int damage, float speed);

        void Update()
        {
            if (target)
            {
                if (target.IsAlive)
                {
                    targetPos = target.transform.position;
                    return;
                }

                target = null;
            }
        }

        protected void InvokeHit()
        {
            Hit?.Invoke();
            Reset();
        }

        void Reset()
        {
            target = null;
            Hit = null;
        }
    }
}