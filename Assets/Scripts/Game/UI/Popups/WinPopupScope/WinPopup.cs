using System;
using DG.Tweening;
using Game.Currencies;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities.Attributes;

namespace Game.UI.Popups.WinPopupScope
{
    public class WinPopup: Popup
    {
        [SerializeField]
        CanvasGroup rewardGroup;

        [SerializeField]
        RectTransform rewardGroupRect;
        
        [SerializeField]
        TextMeshProUGUI valueTmp;
        
        [SerializeField]
        TextMeshProUGUI headerTmp;

        [SerializeField]
        Vector2 headerOffset, rewardOffset, hideOffset;
        
        [SerializeField]
        float showHeaderDuration, showRewardDuration;
        
        [SerializeField]
        float stayDuration;
        
        [SerializeField]
        float hideDuration;
        
        public void Draw(CurrencyItem item)
        {
            valueTmp.text = item.AmountText;
        }
        
        public void Animate(Action callback)
        {
            rewardGroup.alpha = 0;
            var sequence = DOTween.Sequence();
            
            var headerColor = headerTmp.color;
            var headerRect = headerTmp.rectTransform;
            var headerStartPos = headerRect.anchoredPosition + headerOffset;
            var headerShowPos = headerRect.anchoredPosition;
            sequence.Append(DOTween.To(()=> (float)0, x =>
            {
                headerColor.a = x;
                headerTmp.color = headerColor;
                headerRect.anchoredPosition = Vector2.Lerp(headerStartPos, headerShowPos, x);
            }, 1, showHeaderDuration));
            
            var rewardGroupStartPos = rewardGroupRect.anchoredPosition + rewardOffset;
            var rewardGroupShowPos = rewardGroupRect.anchoredPosition;
            sequence.Append(DOTween.To(()=> (float)0, x =>
            {
                rewardGroupRect.anchoredPosition = Vector2.Lerp(rewardGroupStartPos, rewardGroupShowPos, x);
                rewardGroup.alpha = x;
            }, 1, showRewardDuration));
            
            sequence.AppendInterval(stayDuration);

            var headerHidePos = headerRect.anchoredPosition + hideOffset;
            var rewardGroupHidePos = rewardGroupRect.anchoredPosition - hideOffset;
            sequence.Append(DOTween.To(()=> (float)1, x =>
            {
                headerColor.a = x;
                headerTmp.color = headerColor;
                rewardGroup.alpha = x;
                rewardGroupRect.anchoredPosition = Vector2.Lerp(rewardGroupShowPos, rewardGroupHidePos, 1-x);
                headerRect.anchoredPosition = Vector2.Lerp(headerShowPos, headerHidePos, 1-x);
            }, 0, hideDuration)).OnComplete(()=> callback?.Invoke());
        }
    }
}