using Game.Characters.Spawners;
using Game.Signals;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    public class AlliesInitializer: MonoBehaviour
    {
        [SerializeField]
        Castle castle;
        
        [SerializeField]
        TargetsDetectionArea allyDetectionArea;
        
        [SerializeField]
        AllyArchersSpawner allyArchersSpawner;
        
        [SerializeField]
        AllyMeleeCharacterSpawner allyMeleeSpawner;
        
        [SerializeField]
        TowersInitializer towersInitializer;

        SignalBus signalBus;
        
        [Inject]
        void Constructor(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void Init()
        {
            castle.Init();
            castle.Die += OnCastleDied;
            towersInitializer.Init();
            allyDetectionArea.Init(100);
            
            allyMeleeSpawner.Init(signalBus);
            allyArchersSpawner.Init(signalBus);
            allyMeleeSpawner.SpawnAllUnits();
            allyArchersSpawner.SpawnAllUnits();
            
            signalBus.Subscribe<WaveFinishedSignal>(OnWaveFinished);
            signalBus.Subscribe<WaveLaunchedSignal>(OnWaveLaunched);
            signalBus.Subscribe<ResetGameBoard>(OnResetGameBoard);
        }

        void OnWaveLaunched()
        {
            allyDetectionArea.Activate();
            allyDetectionArea.SetMaxTargetsRange(allyArchersSpawner.ArchersCount);
        }
        
        void OnWaveFinished()
        {
            allyDetectionArea.Deactivate();
            towersInitializer.SetTowersIdleState();
            allyMeleeSpawner.SetAllUnitsIdleState();
            allyArchersSpawner.SetAllUnitsIdleState();
        }

        void OnResetGameBoard()
        {
            allyMeleeSpawner.RestoreUnits();
        }
        
        void OnCastleDied()
        {
            signalBus.Fire(new WaveFinishedSignal(false));
        }
    }
}