using System;
using Game.Characters.Units;
using UnityEngine;

namespace Game.Characters.Projectiles
{
    public abstract class Projectile: MonoBehaviour
    {
        protected IDamageable target;
        protected Vector2 targetPos;
        Transform targetTransform;
        string targetId;

        protected bool TargetMissed => target == null || !target.IsAlive || !target.Id.Equals(targetId);
        
        public event Action Flew;

        public abstract void Launch(ProjectileAnimationData animationData, float damage, float speed);

        public void Init(IDamageable target)
        {
            this.target = target;
            targetTransform = target.Transform;
            targetPos = targetTransform.position;
            targetId = target.Id;
        }

        void Update()
        {
            if (target != null)
            {
                if (!TargetMissed)
                {
                    targetPos = targetTransform.position;
                    return;
                }

                target = null;
            }
        }

        protected void OnFlew()
        {
            Flew?.Invoke();
            Reset();
        }

        void Reset()
        {
            target = null;
            targetTransform = null;
            targetId = String.Empty;
            Flew = null;
        }
    }
}