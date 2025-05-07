using DG.Tweening;
using UnityEngine;

namespace Game.Characters.Projectiles
{

    public class Arrow: Projectile 
    {
        public override void Launch(Health target, ProjectileAnimationData animationData, int damage, float speed)
        {
            this.target = target;
            var yOffset = animationData.YOffset;
            var yCurve = animationData.YCurve;
            var xCurve = animationData.XCurve;
            Vector2 startPosition = transform.position;
            Vector2 previousPosition = startPosition;
            
            transform.LookAt(Vector2.Lerp(startPosition, targetPos, 0.01f));
            float distance = Vector2.Distance(startPosition, target.transform.position);
            float duration = distance / speed;
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
            }, 1, duration).OnComplete(()=> OnFlew(damage));
        }

        void OnFlew(int damage)
        {
            if (target && target.IsAlive)
            {
                target.GetDamage(damage);
            }
            InvokeHit();
        }
    }
}