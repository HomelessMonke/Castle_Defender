using System.Collections.Generic;
using System.Linq;
using Game.Currencies;
using Game.Grades;
using Zenject;

namespace Game.UI.Popups.UpgradesPopupScope
{
    public class UpgradesPopupPresenter
    {
        UpgradesPopup popup;
        PopupManager popupManager;
        CurrencyManager currencyManager;
        AllGradesSequenceList gradesSequenceList;

        HashSet<ParameterGrades> parametersToSave = new();
        
        [Inject]
        public UpgradesPopupPresenter(PopupManager popupManager, CurrencyManager currencyManager, AllGradesSequenceList gradesSequenceList)
        {
            this.popupManager = popupManager;
            this.currencyManager = currencyManager;
            this.gradesSequenceList = gradesSequenceList;
        }
        
        public void OpenPopup()
        { 
            popupManager.OpenPopup<UpgradesPopup>(nameof(UpgradesPopup), (p) =>
            {
                popup = p;
                p.BuyClick += OnBuyClick;
                p.CloseClick += OnCloseClick;
                
                ParameterGradesSequence[] sequences = gradesSequenceList.GetNotCompletedSequences.ToArray();
                p.Init();
                p.Draw(sequences, currencyManager);
            });
        }

        void OnBuyClick(UpgradeView view, ParameterGradesSequence sequence)
        {
            var parameter = sequence.GetParameterToUpgrade();
            var currencyToUpgrade = parameter.CurrencyToUpgrade;
            if (currencyManager.Spend(currencyToUpgrade))
            {
                parameter.Upgrade();
                parametersToSave.Add(parameter);
                view.AnimateHide(()=> OnHideAnimated(view, sequence));
            }
        }

        void OnHideAnimated(UpgradeView view, ParameterGradesSequence sequence)
        {
            if (sequence.IsCompleted)
            {
                popup.HideView(view);
                return;
            }
            
            var parameterToDraw = sequence.GetParameterToUpgrade();
            view.Draw(parameterToDraw, currencyManager, sequence.Level);
            popup.RedrawViewsButtonStateExceptView(currencyManager, view);
            view.AnimateShow();
        }

        void SaveProgress()
        {
            foreach (var parameter in parametersToSave)
            {
                parameter.Save();
            }
            
            if(parametersToSave.Count != 0)
                currencyManager.SaveCurrencies();
        }

        void OnCloseClick()
        {
            popup.Close();
            SaveProgress();
        }
    }
}