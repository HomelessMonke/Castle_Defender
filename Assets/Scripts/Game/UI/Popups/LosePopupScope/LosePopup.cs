using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.UI.Popups.LosePopupScope
{
    public class LosePopup: Popup
    {
        [SerializeField]
        TextMeshProUGUI headerTmp;

        [SerializeField]
        Vector2 offsetPos;

        [SerializeField]
        float showDuration, stayDuration, hideDuration;

        public void Animate(Action callback = null)
        {
            var sequence = DOTween.Sequence();
            
            var headerColor = headerTmp.color;
            var headerRect = headerTmp.rectTransform;
            var headerStartPos = headerRect.anchoredPosition + offsetPos;
            var headerShowPos = headerRect.anchoredPosition;
            var headerHidePos = headerShowPos - offsetPos;
            headerRect.anchoredPosition += offsetPos;

            sequence.Append(DOTween.To(() => (float)0, x =>
            {
                headerColor.a = x;
                headerTmp.color = headerColor;
                headerRect.anchoredPosition = Vector2.Lerp(headerStartPos, headerShowPos, x);
            }, 1, showDuration));
            
            sequence.AppendInterval(stayDuration);

            sequence.Append(DOTween.To(() => (float)1, x =>
            {
                headerColor.a = x;
                headerTmp.color = headerColor;
                headerRect.anchoredPosition = Vector2.Lerp(headerShowPos, headerHidePos, 1-x);
            }, 0, hideDuration).OnComplete(()=> callback?.Invoke()));
        }
    }
}