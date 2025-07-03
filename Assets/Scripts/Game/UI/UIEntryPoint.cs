using Game.Characters.Spawners;
using Game.Signals;
using Game.UI.Abilities;
using Game.UI.BaseUiScope;
using Game.UI.Currencies;
using Game.UI.Popups;
using Game.UI.Popups.PausePopupScope;
using Game.UI.Popups.StartWavePopupScope;
using Game.UI.Popups.UpgradesPopupScope;
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
        WavesManager wavesManager;
        
        [SerializeField]
        CurrenciesPanel currenciesPanel;

        [SerializeField]
        Button startWaveButton;

        [SerializeField]
        Button pauseButton;
        
        [SerializeField]
        SpeedChangeButton speedChangeButton;
        
        [SerializeField]
        Button upgradesButton;
        
        [SerializeField]
        AbilitiesPanel abilitiesPanel;

        [SerializeField]
        PopupManager popupManager;

        UpgradesPopupFactory upgradesPresenterFactory;
        PausePopupFactory pausePopupFactory;
        
        SignalBus signalBus;

        [Inject]
        void Construct(SignalBus signalBus, UpgradesPopupFactory upgradesPresenterFactory, PausePopupFactory pausePopupFactory)
        {
            this.signalBus = signalBus;
            this.upgradesPresenterFactory = upgradesPresenterFactory;
            this.pausePopupFactory = pausePopupFactory;
        }

        public void Init()
        {
            baseUI.Init();
            abilitiesPanel.Init();
            currenciesPanel.Init();
            speedChangeButton.Draw();
            currenciesPanel.DrawViews();
            
            popupManager.ConfigChanged += OnPopupConfigChanged;
            signalBus.Subscribe<WaveFinishedSignal>(OnWaveFinished);
            
            pauseButton.onClick.AddListener(OnPauseClick);
            startWaveButton.onClick.AddListener(OnStartWaveClick);
            speedChangeButton.Button.onClick.AddListener(OnToggledSpeedClick);
            upgradesButton.onClick.AddListener(OnUpgradesClick);
        }

        void OnStartWaveClick()
        {
            baseUI.SetInBattleConfig();
            wavesManager.LaunchNextWave();
        }
        
        void OnUpgradesClick()
        {
            var presenter = upgradesPresenterFactory.Create();
            presenter.OpenPopup();
        }
        
        void OnPauseClick()
        {
            var presenter = pausePopupFactory.Create();
            presenter.OpenPopup();
        }

        void OnToggledSpeedClick()
        {
            GameSpeed.ToggleSpeed();
            speedChangeButton.Draw();
        }

        void OnPopupConfigChanged(PopupConfig config)
        {
            baseUI.Switch(config);
        }

        void OnWaveFinished()
        {
            GameSpeed.ResetSpeed();
            speedChangeButton.Draw();
        }
    }
}