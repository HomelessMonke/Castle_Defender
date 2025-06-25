using Game.Signals;
using Zenject;

namespace Game.Waves
{
    public class WaveProgressCounter
    {
        int counter;
        
        SignalBus signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void Init()
        {
            signalBus.Subscribe<LaunchWaveSignal>(OnLaunchWave);
            signalBus.Subscribe<DespawnEnemySignal>(OnDespawnEnemy);
        }

        void OnLaunchWave(LaunchWaveSignal signal)
        {
            counter = signal.CharactersCount;
        }
        
        void OnDespawnEnemy()
        {
            counter--;
            if (counter == 0)
            {
                signalBus.Fire<FinishWaveSignal>();
            }
        }
    }
}