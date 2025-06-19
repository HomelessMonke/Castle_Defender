using Game.Characters.Spawners;
using Game.Popups;
using Game.Signals;
using Game.UI.Abilities;
using Game.UI.BaseUiScope;
using Game.UI.Currencies;
using Game.UI.Popups;
using Game.UI.Popups.UpgradesPopupSpace;
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
        
        SignalBus signalBus;

        [Inject]
        void Construct(UpgradesPopupFactory upgradesPresenterFactory, SignalBus signalBus)
        {
            this.upgradesPresenterFactory = upgradesPresenterFactory;
            this.signalBus = signalBus;
        }

        public void Init()
        {
            baseUI.Init();
            abilitiesPanel.Init();
            currenciesPanel.Init();
            currenciesPanel.DrawViews();
            
            popupManager.ConfigChanged += OnPopupConfigChanged;
            signalBus.Subscribe<WaveFinishedSignal>(OnWaveFinished);
                
            startWaveButton.onClick.AddListener(OnStartWaveClick);
            upgradesButton.onClick.AddListener(OnUpgradesClick);
        }
        
        void OnStartWaveClick()
        {
            baseUI.SwitchInBattleConfig(false);
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

        void OnWaveFinished()
        {
            baseUI.SwitchPreBattleConfig();
        }
    }
}