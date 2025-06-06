using System;
using Game.Grades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups.UpgradesPopupSpace
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField]
        Image image;
        
        [SerializeField]
        TextMeshProUGUI headerTmp;
        
        [SerializeField]
        TextMeshProUGUI descriptionTmp;
        
        [SerializeField]
        TextMeshProUGUI valueText;

        [SerializeField]
        Button buyButton;
        
        public event Action BuyClick;

        public void Init()
        {
            buyButton.onClick.AddListener(() => { BuyClick?.Invoke(); });
        }

        public void Draw(string headerText, ParameterGrades parameter)
        {
            headerTmp.text = headerText;
            Draw(parameter);
        }
        
        public void Draw(ParameterGrades parameter)
        {
            image.sprite = parameter.Sprite;
            descriptionTmp.text = parameter.LocalizedDescription;
            valueText.text = parameter.CurrencyToUpgrade.ToString();
        }
    }
}