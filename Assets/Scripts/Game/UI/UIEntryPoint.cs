using Game.Characters.Spawners;
using Game.Popups;
using Game.Signals;
using Game.UI.Abilities;
using Game.UI.BaseUiScope;
using Game.UI.Currencies;
using Game.UI.Popups;
using Game.UI.Popups.StartWavePopupSpace;
using Game.UI.Popups.UpgradesPopupSpace;
using Game.Waves;
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
        WavesList wavesList;
        
        [SerializeField]
        WavesSpawner wavesSpawner;
        
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
        StartWavePopupFactory startWavePresenterFactory;
        
        SignalBus signalBus;

        [Inject]
        void Construct(SignalBus signalBus, UpgradesPopupFactory upgradesPresenterFactory, StartWavePopupFactory startWavePresenterFactory)
        {
            this.signalBus = signalBus;
            this.upgradesPresenterFactory = upgradesPresenterFactory;
            this.startWavePresenterFactory = startWavePresenterFactory;
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
            baseUI.SetInBattleConfig();
            wavesSpawner.LaunchNextWave();
            
            var presenter = startWavePresenterFactory.Create();
            presenter.OpenPopup(wavesList.WaveNumber);
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