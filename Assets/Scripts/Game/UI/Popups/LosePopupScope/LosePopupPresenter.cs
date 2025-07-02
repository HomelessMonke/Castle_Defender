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

        public void OpenPopup()
        {
            popupManager.ShowDarkPanel();
            popupManager.OpenPopup<LosePopup>(nameof(LosePopup), p =>
            {
                p.Animate(()=> OnPopupAnimated(p));
            });
        }

        void OnPopupAnimated(Popup p)
        {
            popupManager.HideDarkPanel();
            p.Close();
        }
    }
}