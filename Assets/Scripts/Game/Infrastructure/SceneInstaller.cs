using Game.UI.Popups;
using Game.UI.Popups.PausePopupScope;
using Game.UI.Popups.StartWavePopupScope;
using Game.UI.Popups.UpgradesPopupScope;
using Game.UI.Popups.WinPopupScope;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
    public class SceneInstaller: MonoInstaller
    {
        [SerializeField]
        PopupManager popupManager;
        
        public override void InstallBindings()
        {
            BindPopupManager();
            BindPopupPresenters();
        }

        void BindPopupManager()
        {
            Container.Bind<PopupManager>().FromInstance(popupManager);
        }

        void BindPopupPresenters()
        {
            Container.BindFactory<UpgradesPopupPresenter, UpgradesPopupFactory>().AsTransient();
            Container.BindFactory<PausePopupPresenter, PausePopupFactory>().AsTransient();
            Container.BindFactory<StartWavePopupPresenter, StartWavePopupFactory>().AsTransient();
            Container.BindFactory<WinPopupPresenter, WinPopupFactory>().AsTransient();
        }
    }
}