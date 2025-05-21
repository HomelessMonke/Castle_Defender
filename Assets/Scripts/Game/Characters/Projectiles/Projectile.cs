using System;
using UnityEngine;

namespace Game.Characters.Projectiles
{
    public abstract class Projectile: MonoBehaviour
    {
        protected Health target;
        protected Vector2 targetPos;
        
        public event Action Hit;

        public abstract void Launch(Health target, ProjectileAnimationData animationData, float damage, float speed);

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