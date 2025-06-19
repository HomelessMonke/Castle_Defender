using Game.Currencies;
using Game.Signals;
using Game.Waves;
using Zenject;

namespace Game.Infrastructure
{
    public class GameInitializer
    {
        [Inject]
        CurrencyManager currencyManager; 
        
        [Inject]
        WaveProgressCounter waveProgressCounter;
        
        [Inject]
        SignalBus signalBus;
        
        public void Init()
        {
            signalBus.Subscribe<WaveFinishedSignal>(OnWaveFinished);
            waveProgressCounter.Init();
        }

        void OnWaveFinished()
        {
            currencyManager.SaveCurrencies();
        }
    }
}