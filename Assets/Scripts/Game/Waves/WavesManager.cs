using Game.Currencies;
using Game.Signals;
using Game.UI.BaseUiScope;
using Game.UI.Popups.LosePopupScope;
using Game.UI.Popups.StartWavePopupScope;
using Game.UI.Popups.WinPopupScope;
using UnityEngine;
using Zenject;

namespace Game.Waves
{
    /// <summary>
    /// Запускает следующу волну
    /// Отправляет событие начало волны
    /// Обрабатывает окончание волны, подписывает открытие окна победы/поражения
    /// </summary>
    public class WavesManager: MonoBehaviour
    {
        [SerializeField]
        BaseUI baseUI;
        
        [SerializeField]
        WavesList wavesList;
        
        [SerializeField]
        WavesSpawner wavesSpawner;
        
        WaveProgressCounter waveUnitsCounter = new();
        CurrencyManager currencyManager; 
        SignalBus signalBus;

        [Inject]
        StartWavePopupFactory startWavePresenterFactory;
        
        [Inject]
        LosePopupFactory losePopupFactory;
        
        [Inject]
        WinPopupFactory winPopupFactory;
        
        [Inject]
        void Construct(CurrencyManager currencyManager, SignalBus signalBus)
        {
            this.currencyManager = currencyManager;
            this.signalBus = signalBus;
        }

        void Start()
        {
            waveUnitsCounter.Init(signalBus);
            signalBus.Subscribe<WaveFinishedSignal>(OnWaveFinished);
            signalBus.Subscribe<DespawnEnemySignal>(waveUnitsCounter.DecreaseCount);
        }

        public void LaunchNextWave()
        {
            var presenter = startWavePresenterFactory.Create();
            presenter.OpenPopup(wavesList.WaveNumber, OnStartWavePopupClosed);
        }

        void OnStartWavePopupClosed()
        {
            var wave = wavesList.NextWave;
            waveUnitsCounter.SetCount(wave.CharactersCount);
            wavesSpawner.LaunchWave(wave);
            signalBus.Fire<WaveLaunchedSignal>();
        }

        void OnWaveFinished(WaveFinishedSignal signal)
        {
            baseUI.SetPreBattleConfig();
            if(signal.IsVictory)
                WinHandle();
            else
                LoseHandle();
        }

        void WinHandle()
        {
            var rewardCurrency = wavesList.NextWave.RewardCurrency;
            var presenter = winPopupFactory.Create();
            presenter.OpenPopup(rewardCurrency, ()=>
            {
                currencyManager.Earn(rewardCurrency);
                currencyManager.SaveCurrencies();
                wavesList.IncreaseNextWaveIndex();
                wavesList.SaveWavesData();
                signalBus.Fire<ResetGameBoard>();
            });
        }

        void LoseHandle()
        {
            var presenter = losePopupFactory.Create();
            presenter.OpenPopup(()=>
            {
                currencyManager.SaveCurrencies();
                signalBus.Fire<ResetGameBoard>();
            });
        }
    }
}