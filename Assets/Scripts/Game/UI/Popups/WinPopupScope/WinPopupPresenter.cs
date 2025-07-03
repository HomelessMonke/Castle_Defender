using System;
using Game.Currencies;
using Zenject;

namespace Game.UI.Popups.WinPopupScope
{
    public class WinPopupPresenter
    {
        PopupManager popupManager;
        
        [Inject]
        public WinPopupPresenter(PopupManager popupManager)
        {
            this.popupManager = popupManager;
        }

        public void OpenPopup(CurrencyItem item, Action closeCallback)
        {
            popupManager.ShowDarkPanel();
            popupManager.OpenPopup<WinPopup>(nameof(WinPopup), p =>
            {
                p.Draw(item);
                p.Animate(()=> OnPopupAnimated(p, item, closeCallback));
            });
        }
        
        void OnPopupAnimated(Popup p, CurrencyItem item, Action closeCallback)
        {
            popupManager.HideDarkPanel();
            p.Close();
            closeCallback?.Invoke();
        }
    }
}