using System;
using Game.Characters.Spawners;
using Game.Popups;
using Game.UI.BaseUiScope;
using Game.UI.Currencies;
using Game.UI.Popups;
using Game.UI.Popups.UpgradesPopupSpace;
using UI.Abilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class UIEntryPoint: MonoBehaviour
    {
        [SerializeField]
        BaseUI baseUI;
        
        [SerializeField]
        WavesSpawner wavesManager;
        
        [SerializeField]
        CurrenciesPanel currenciesPanel;

        [SerializeField]
        Button startWaveButton;
        
        [SerializeField]
        Button upgradesButton;
        
        [SerializeField]
        AbilitiesPanel abilitiesPanel;

        [SerializeField]
        PopupManager popupManager;

        UpgradesPopupFactory upgradesPresenterFactory;

        [Inject]
        void Construct(UpgradesPopupFactory upgradesPresenterFactory)
        {
            this.upgradesPresenterFactory = upgradesPresenterFactory;
        }

        public void Init()
        {
            baseUI.Init();
            abilitiesPanel.Init();
            currenciesPanel.DrawViews();
            popupManager.ConfigChanged -= OnPopupConfigChanged;
            popupManager.ConfigChanged += OnPopupConfigChanged;
            
            startWaveButton.onClick.AddListener(OnStartWaveClick);
            upgradesButton.onClick.AddListener(OnUpgradesClick);
        }
        
        void OnStartWaveClick()
        {
            baseUI.SwitchInBattleConfig();
            wavesManager.LaunchNextWave();
        }
        
        void OnUpgradesClick()
        {
            var presenter = upgradesPresenterFactory.Create();
            presenter.OpenPopup();
        }

        void OnPopupConfigChanged(PopupConfig config)
        {
            baseUI.Switch(config);
        }
    }
}