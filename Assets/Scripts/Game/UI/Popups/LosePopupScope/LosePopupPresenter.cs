using System;
using Zenject;

namespace Game.UI.Popups.LosePopupScope
{
    public class LosePopupPresenter
    {
        PopupManager popupManager;
        
        [Inject]
        public LosePopupPresenter(PopupManager popupManager)
        {
            this.popupManager = popupManager;
        }

        public void OpenPopup(Action closeCallback)
        {
            popupManager.ShowDarkPanel();
            popupManager.OpenPopup<LosePopup>(nameof(LosePopup), p =>
            {
                p.Animate(()=> OnPopupAnimated(p, closeCallback));
            });
        }

        void OnPopupAnimated(Popup p, Action closeCallback)
        {
            popupManager.HideDarkPanel();
            p.Close();
            closeCallback?.Invoke();
        }
    }
}