using System.Collections.Generic;
using System.Linq;
using Game.Currencies;
using Game.Grades;
using Game.Popups;
using Zenject;

namespace Game.UI.Popups.UpgradesPopupSpace
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
                p.Draw(sequences);
            });
        }

        void OnBuyClick(UpgradeView view, ParameterGradesSequence sequence)
        {
            var grades = sequence.GetParameterToUpgrade();
            var currencyToUpgrade = grades.CurrencyToUpgrade;
            if (currencyManager.Spend(currencyToUpgrade))
            {
                grades.Upgrade();
                parametersToSave.Add(grades);
                var parameterToDraw = sequence.GetParameterToUpgrade();
                view.Draw(parameterToDraw, sequence.Level);
            }
            else
            {
                //
            }
        }

        void SaveSequences()
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
            SaveSequences();
        }
    }
}