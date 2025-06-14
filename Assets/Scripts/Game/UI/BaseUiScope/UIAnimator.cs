using System;
using DG.Tweening;
using UnityEngine;

namespace Game.UI.BaseUiScope
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIAnimator : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectTransform;
        
        [SerializeField]
        AnimationCurveObject curveObj;

        [SerializeField]
        CanvasGroup canvasGroup;
        
        Tween moveTween, alphaTween;
        
        public void AnimateShow(Vector2 startPos, Vector2 targetPos, float duration, Action complete = null)
        {
            AnimateAlpha(0, 1, duration);
            AnimateMove(startPos, targetPos, duration, complete);
        }
        
        public void AnimateHide(Vector2 startPos, Vector2 targetPos, float duration, Action complete = null)
        {
            AnimateAlpha(1, 0, duration);
            AnimateMove(startPos, targetPos, duration, complete);
        }
        
        public void AnimateMove(Vector2 startPos, Vector2 targetPos, float duration, Action complete = null)
        {
            gameObject.SetActive(true);
            
            if (moveTween != null && moveTween.IsActive())
                moveTween.Kill();
            
            moveTween = DOTween.To(() => (float)0, x =>
            {
                rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, curveObj.Curve.Evaluate(x));
            }, 1, duration).SetAutoKill(true).OnComplete(()=> complete?.Invoke());
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