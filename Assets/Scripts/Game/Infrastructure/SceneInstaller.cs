using System;
using Game.Popups;
using Game.UI.Popups.StartWavePopupSpace;
using Game.UI.Popups.PausePopupSpace;
using Game.UI.Popups.UpgradesPopupSpace;
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
        }
    }
}