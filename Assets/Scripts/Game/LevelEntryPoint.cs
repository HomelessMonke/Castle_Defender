using Game.Characters;
using Game.Characters.Spawners;
using Game.Signals;
using Game.UI;
using Game.Waves;
using UnityEngine;
using Zenject;

namespace Game
{
    public class LevelEntryPoint: MonoBehaviour
    {
        [SerializeField]
        EnemySpawnersList enemySpawnersList;

        [SerializeField]
        AlliesInitializer alliesInitializer;
        
        [SerializeField]
        ProjectileSpawnersList projectileSpawnersList;
        
        [SerializeField]
        WavesList wavesList;
        
        [SerializeField]
        UIEntryPoint uiEntryPoint;

        SignalBus signalBus;
        
        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        
        public void Start()
        {
            uiEntryPoint.Init();
            
            projectileSpawnersList.Init();
            alliesInitializer.Init();
            enemySpawnersList.Init();
            
            signalBus.Subscribe<FinishWaveSignal>(OnWaveFinishedSignal);
        }

        void OnWaveFinishedSignal()
        {
            wavesList.IncreaseNextWaveIndex();
            wavesList.SaveWavesData();
        }
    }
}