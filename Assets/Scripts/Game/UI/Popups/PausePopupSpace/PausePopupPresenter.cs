using Game.Popups;
using Zenject;

namespace Game.UI.Popups.PausePopupSpace
{
    public class PausePopupPresenter
    {
        PausePopup popup;
        PopupManager popupManager;

        [Inject]
        public PausePopupPresenter(PopupManager popupManager)
        {
            this.popupManager = popupManager;
        }

        public void OpenPopup()
        {
            popupManager.ShowDarkPanel();
            popupManager.OpenPopup<PausePopup>(nameof(PausePopup), p =>
            {
                popup = p;
                p.Init();
                GameSpeed.TogglePause(true);
                p.ResumeClick += OnResumeClick;
            });
        }
        
        void OnResumeClick()
        {
            GameSpeed.TogglePause(false);
            popupManager.HideDarkPanel();
            popup.Close();
        }
    }
}
