using System;
using DG.Tweening;
using UnityEngine;

namespace Game.UI.BaseUiScope
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIAnimator : MonoBehaviour
    {
        [SerializeField]
        AnimationCurveObject curveObj;

        [SerializeField]
        CanvasGroup canvasGroup;
        
        Tween moveTween, alphaTween;

        public void AnimateMove(Vector3 startPos, Vector3 targetPos, float duration, Action complete = null)
        {
            gameObject.SetActive(true);
            
            if (moveTween != null && moveTween.IsActive())
                moveTween.Kill();
            
            moveTween = DOTween.To(() => (float)0, x =>
            {
                transform.position = Vector3.Lerp(startPos, targetPos, curveObj.Curve.Evaluate(x));
            }, 1, duration).SetAutoKill(true).OnComplete(()=> complete?.Invoke());
        }

        public void AnimateShow(Vector3 startPos, Vector3 targetPos, float duration, Action complete = null)
        {
            AnimateMove(startPos, targetPos, duration, complete);
            AnimateAlpha(0, 1, duration);
        }
        
        public void AnimateHide(Vector3 startPos, Vector3 targetPos, float duration, Action complete = null)
        {
            AnimateMove(startPos, targetPos, duration, complete);
            AnimateAlpha(1, 0, duration);
        }

        void AnimateAlpha(float from, float to, float duration, Action complete = null)
        {
            gameObject.SetActive(true);

            if (alphaTween != null && alphaTween.IsActive())
                alphaTween.Kill();
            
            alphaTween = DOTween.To(() => (float)0, x =>
            {
               canvasGroup.alpha = Mathf.Lerp(from, to, curveObj.Curve.Evaluate(x));
            }, 1, duration).SetAutoKill(true).OnComplete(()=> complete?.Invoke());
        }
    }
}