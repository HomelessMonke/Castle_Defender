using System;
using System.Collections.Generic;
using Game.Currencies;
using Game.Grades;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Extensions;

namespace Game.UI.Popups.UpgradesPopupSpace
{
    public class UpgradesPopup : Popup
    {
        [SerializeField]
        Transform root;
        
        [SerializeField]
        UpgradeView viewTemplate;

        [SerializeField]
        LayoutGroup layoutGroup;
        
        [SerializeField]
        Button closeButton;

        List<UpgradeView> views;
        
        public event Action<UpgradeView, ParameterGradesSequence> BuyClick;
        public event Action CloseClick;
        
        public void Init()
        {
            closeButton.onClick.AddListener(()=> CloseClick?.Invoke());
        }

        public void Draw(ParameterGradesSequence[] sequences, CurrencyManager currencyManager)
        {
            root.DestroyChildren();
            views = new List<UpgradeView>();
            foreach (var sequence in sequences)
            {
                var parameterUpgrade = sequence.GetParameterToUpgrade();
                if(!parameterUpgrade)
                    continue;

                var view = Instantiate(viewTemplate, root);
                var seq = sequence;
                view.BuyClick += ()=> BuyClick?.Invoke(view, seq);
                view.Init();
                view.gameObject.SetActive(true);
                view.Draw(parameterUpgrade, currencyManager, sequence.Level, sequence.HeaderText);
                views.Add(view);
            }
            viewTemplate.gameObject.SetActive(false);
        }

        public void RedrawViewsButtonStateExceptView(CurrencyManager currencyManager, UpgradeView exceptView)
        {
            foreach (var view in views)
            {
                if(view.Equals(exceptView))
                    continue;

                view.DrawButtonState(currencyManager);
            }
        }

        public void HideView(UpgradeView view)
        {
            view.gameObject.SetActive(false);
            views.Remove(view);
        }
    }
}