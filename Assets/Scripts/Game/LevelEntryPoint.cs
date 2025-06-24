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
        Castle castle;
        
        [SerializeField]
        UIHealthView castleHpView;

        [SerializeField]
        TargetsDetectionArea allyDetectionArea;
        
        [SerializeField]
        EnemySpawnersList enemySpawnersList;

        [SerializeField]
        AllySpawnersList allySpawnersList;
        
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
            castle.Init();
            uiEntryPoint.Init();
            allySpawnersList.Init();
            enemySpawnersList.Init();
            
            allyDetectionArea.Init(128);
            
            signalBus.Subscribe<WaveFinishedSignal>(OnWaveFinishedSignal);
        }

        void OnWaveFinishedSignal()
        {
            wavesList.IncreaseNextWaveIndex();
            wavesList.SaveWavesData();
        }
    }
}