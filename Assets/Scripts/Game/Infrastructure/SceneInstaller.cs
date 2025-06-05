using Game.Popups;
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
        }

        void BindPopupManager()
        {
            Container.Bind<PopupManager>().FromInstance(popupManager);
        }
    }
}