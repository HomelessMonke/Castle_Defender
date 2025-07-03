using System;
using Zenject;

namespace Game.UI.Popups.StartWavePopupScope
{
    public class StartWavePopupPresenter
    {
        PopupManager popupManager;
        
        [Inject]
        public StartWavePopupPresenter(PopupManager popupManager)
        {
            this.popupManager = popupManager;
        }

        public void OpenPopup(int waveNumber, Action closeCallback)
        {
            popupManager.ShowDarkPanel();
            popupManager.OpenPopup<StartWavePopup>(nameof(StartWavePopup), p =>
            {
                p.DrawWaveNumber(waveNumber);
                p.AnimatePopup(() => OnPopupAnimated(p, closeCallback));
            });
        }

        void OnPopupAnimated(Popup p, Action closeCallback)
        {
            p.Close();
            popupManager.HideDarkPanel();
            closeCallback?.Invoke();
        }
    }
}