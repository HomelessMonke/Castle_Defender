using Game.Currencies;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using Zenject;

namespace Game.UI.Popups.WinPopupScope
{
    public class WinPopupPresenter
    {
        PopupManager popupManager;
        CurrencyManager currencyManager;
        
        [Inject]
        public WinPopupPresenter(PopupManager popupManager,CurrencyManager currencyManager)
        {
            this.popupManager = popupManager;
            this.currencyManager = currencyManager;
        }

        public void OpenPopup(CurrencyItem item)
        {
            popupManager.ShowDarkPanel();
            popupManager.OpenPopup<WinPopup>(nameof(WinPopup), p =>
            {
                p.Draw(item);
                p.Animate(()=> OnPopupAnimated(p, item));
            });
        }
        
        void OnPopupAnimated(Popup p, CurrencyItem item)
        {
            currencyManager.Earn(item);
            popupManager.HideDarkPanel();
            p.Close();
        }
    }
}