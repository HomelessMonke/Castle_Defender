using Game.Popups;
using Zenject;

namespace Game.UI.Popups.StartWavePopupSpace
{
    public class StartWavePopupPresenter
    {
        PopupManager popupManager;
        
        [Inject]
        public StartWavePopupPresenter(PopupManager popupManager)
        {
            this.popupManager = popupManager;
        }

        public void OpenPopup(int waveNumber)
        {
            popupManager.ShowDarkPanel();
            popupManager.OpenPopup<StartWavePopup>(nameof(StartWavePopup), p =>
            {
                p.DrawWaveNumber(waveNumber);
                p.AnimatePopup(() => OnPopupAnimated(p));
            });
        }

        void OnPopupAnimated(Popup p)
        {
            p.Close();
            popupManager.HideDarkPanel();
        }
    }
}