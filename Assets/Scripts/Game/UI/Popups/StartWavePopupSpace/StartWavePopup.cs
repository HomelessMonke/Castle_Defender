using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Game.UI.Popups.StartWavePopupSpace
{
    public class StartWavePopup: Popup
    {
        [SerializeField]
        TextMeshProUGUI waveNumberTMP;

        [SerializeField]
        LocalizedString waveLocalization;
        
        [SerializeField]
        AnimationCurve showCurve, hideCurve;
        
        [SerializeField]
        float delayDuration, showDuration, stayDuration, hideDuration;

        [SerializeField]
        Vector2 positionOffset;
        
        public void DrawWaveNumber(int waveNumber)
        {
            waveNumberTMP.text = $"{waveLocalization.GetLocalizedString()} {waveNumber}";
        }
        
        public void AnimatePopup(Action onComplete)
        {
            var sequence = DOTween.Sequence();
            var color = waveNumberTMP.color;
            var waveRect = waveNumberTMP.rectTransform;
            var centerPos = waveRect.anchoredPosition;
            var startPos = centerPos + positionOffset;
            var endPos = centerPos - positionOffset;

            color.a = 0;
            waveNumberTMP.color = color; 
            sequence.AppendInterval(delayDuration);
            
            sequence.Append(DOTween.To(()=> (float)0, x =>
            {
                waveRect.anchoredPosition = Vector2.Lerp(startPos, centerPos, showCurve.Evaluate(x));
                color.a = Mathf.Lerp(0, 1, showCurve.Evaluate(x));
                waveNumberTMP.color = color;
            }, 1, showDuration)).SetAutoKill(true);

            sequence.AppendInterval(stayDuration);
            
            sequence.Append(DOTween.To(()=> (float)0, x =>
            {
                waveRect.anchoredPosition = Vector2.Lerp(centerPos, endPos, hideCurve.Evaluate(x));
                color.a = Mathf.Lerp(1, 0, hideCurve.Evaluate(x));
                waveNumberTMP.color = color;
            }, 1, hideDuration)).SetAutoKill(true).OnComplete(()=> onComplete?.Invoke());
        }
    }
}