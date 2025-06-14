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
            var upgrades = sequence.GetParameterToUpgrade();
            var currencyToUpgrade = upgrades.CurrencyToUpgrade;
            if (currencyManager.Spend(currencyToUpgrade))
            {
                upgrades.Upgrade();
                var parameterToDraw = sequence.GetParameterToUpgrade();
                view.Draw(parameterToDraw, sequence.Level);
            }
            else
            {
                //
            }
        }

        void OnCloseClick()
        {
            popup.Close();
        }
    }
}