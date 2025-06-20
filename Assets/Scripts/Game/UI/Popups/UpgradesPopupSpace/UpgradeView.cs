using System;
using System.Collections;
using Game.Grades;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Game.UI.Popups.UpgradesPopupSpace
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvasGroup;
        
        [SerializeField]
        Image image;
        
        [SerializeField]
        TextMeshProUGUI headerTmp;
        
        [SerializeField]
        TextMeshProUGUI descriptionTmp;
        
        [SerializeField]
        TextMeshProUGUI buyValueTmp;

        [SerializeField]
        LocalizedString levelLocalization;
        
        [SerializeField]
        TextMeshProUGUI levelTmp;

        [SerializeField]
        Button buyButton;

        [SerializeField]
        UpgradeViewAnimatorData animatorData;
        
        UpgradeViewAnimator animator;
        
        public event Action BuyClick;

        public void Init()
        {
            buyButton.onClick.AddListener(OnBuyClick);
            animator = new UpgradeViewAnimator(animatorData, this, canvasGroup);
        }

        public void Draw(ParameterGrades parameter, int level, string headerText)
        {
            headerTmp.text = headerText;
            Draw(parameter, level);
        }
        
        public void Draw(ParameterGrades parameter, int level)
        {
            image.sprite = parameter.Sprite;
            levelTmp.text = $"{levelLocalization.GetLocalizedString()} {level}";
            descriptionTmp.text = parameter.LocalizedDescription;
            buyValueTmp.text = parameter.CurrencyToUpgrade.AmountText;
        }

        public void AnimateShow()
        {
            animator.Show();
        }
        
        public void AnimateHide(Action onComplete)
        {
            animator.Hide(onComplete);
        }
        
        void OnBuyClick()
        {
            BuyClick?.Invoke();
        }
    }

}