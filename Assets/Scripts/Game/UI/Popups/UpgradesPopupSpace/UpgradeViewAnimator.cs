using System;
using DG.Tweening;
using UnityEngine;

namespace Game.UI.Popups.UpgradesPopupSpace
{
    public class UpgradeViewAnimator
    {
        UpgradeViewAnimatorData data;
        CanvasGroup canvasGroup;
        UpgradeView view;
        
        Tween animationTween;

        public UpgradeViewAnimator(UpgradeViewAnimatorData data, UpgradeView view, CanvasGroup canvasGroup)
        {
            this.data = data;
            this.view = view;
            this.canvasGroup = canvasGroup;
        }

        public void AnimateShowView(Action onComplete = null)
        {
            if (animationTween != null && animationTween.IsActive())
                animationTween.Kill();

            var startScale = data.BeforeShowScale;
            var duration = data.ShowDuration;
            animationTween = DOTween.To(()=> (float)0, x =>
            {
                view.transform.localScale = Vector2.Lerp(startScale, Vector2.one, x);
                canvasGroup.alpha = Mathf.Lerp(0, 1, x);
            }, 1, duration).SetAutoKill(true).OnComplete(()=> OnShowCompleted(onComplete));
        }

        void OnShowCompleted(Action onComplete = null)
        {
            canvasGroup.interactable = true;
            onComplete?.Invoke();
        }

        public void AnimateHideView(Action onComplete = null)
        {
            if (animationTween != null && animationTween.IsActive())
                animationTween.Kill();

            canvasGroup.interactable = false;
            var step1Duration = data.HideDurationStep1;
            var step2Duration = data.HideDurationStep2;
            var foldScale = data.FoldScale;
            var expandScale = data.ExpandScale;
            
            var sequence = DOTween.Sequence();
            sequence.Append(DOTween.To(() => (float)0, x =>
            {
                view.transform.localScale = Vector2.Lerp(Vector2.one, foldScale, x);
            }, 1, step1Duration)).SetAutoKill(true);
            
            sequence.Append(DOTween.To(() => (float)0, x =>
            {
                view.transform.localScale = Vector2.Lerp(foldScale, expandScale, x);
                canvasGroup.alpha = Mathf.Lerp(1, 0, x);
            }, 1, step2Duration)).SetAutoKill(true).OnComplete(()=> onComplete?.Invoke());

            animationTween = sequence;
        }
    }
}