﻿using Audio.Sounds;
using DG.Tweening;
using JSAM;
using UnityEngine;

namespace Game.Characters.Projectiles
{
    public class Arrow: Projectile 
    {
        public override void Launch(ProjectileAnimationData animationData, float damage, float speed)
        {
            var yOffset = animationData.YOffset;
            var yCurve = animationData.YCurve;
            var xCurve = animationData.XCurve;
            Vector2 startPosition = transform.position;
            Vector2 previousPosition = startPosition;
            
            transform.LookAt(Vector2.Lerp(startPosition, targetPos, 0.01f));
            float distance = Vector2.Distance(startPosition, targetPos);
            float duration = distance / speed;
            AudioManager.PlaySound(Sounds.ArrowRelease);
            DOTween.To(() => (float)0, x =>
            {
                var t = xCurve.Evaluate(x);
                var offsetMultiplier = yCurve.Evaluate(x);
                var yMultipliedOffset = new Vector2(0, yOffset * offsetMultiplier);
                var currentPosition = Vector2.Lerp(startPosition, targetPos, t) + yMultipliedOffset;
                transform.position = currentPosition;

                Vector2 direction = currentPosition - previousPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
                previousPosition = currentPosition;
            }, 1, duration).OnComplete(()=> OnFlew(damage)).SetAutoKill(true);
        }

        void OnFlew(float damage)
        {
            if (!TargetMissed)
            {
                AudioManager.PlaySound(Sounds.ArrowImpact);
                target.HealthComponent.GetDamage(damage);
            }
            OnFlew();
        }
    }
}