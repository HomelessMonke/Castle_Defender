using System;
using System.Collections;
using Game.Currencies;
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
        Image buyButtonImage;

        [SerializeField]
        Color canBuyColor, cantBuyColor;
        
        [SerializeField]
        UpgradeViewAnimatorData animatorData;
        
        UpgradeViewAnimator animator;

        CurrencyItem currencyToUpgrade;
        
        public event Action BuyClick;

        public void Init()
        {
            buyButton.onClick.AddListener(OnBuyClick);
            animator = new UpgradeViewAnimator(animatorData, this, canvasGroup);
        }

        public void Draw(ParameterGrades parameter, CurrencyManager currencyManager, int level, string headerText)
        {
            headerTmp.text = headerText;
            Draw(parameter, currencyManager, level);
        }
        
        public void Draw(ParameterGrades parameter, CurrencyManager currencyManager, int level)
        {
            currencyToUpgrade = parameter.CurrencyToUpgrade;
            DrawButtonState(currencyManager);
            
            image.sprite = parameter.Sprite;
            levelTmp.text = $"{levelLocalization.GetLocalizedString()} {level}";
            descriptionTmp.text = parameter.LocalizedDescription;
            buyValueTmp.text = parameter.CurrencyToUpgrade.AmountText;
        }

        public void DrawButtonState(CurrencyManager currencyManager)
        {
            var currencyToBuy = currencyToUpgrade;
            var canBuy = currencyManager.CanSpend(currencyToBuy);
            
            buyButton.interactable = canBuy;
            buyButtonImage.color = canBuy ? canBuyColor : cantBuyColor;
        }

        public void AnimateShow()
        {
            animator.AnimateShowView();
        }
        
        public void AnimateHide(Action onComplete)
        {
            animator.AnimateHideView(onComplete);
        }
        
        void OnBuyClick()
        {
            BuyClick?.Invoke();
        }
    }

}