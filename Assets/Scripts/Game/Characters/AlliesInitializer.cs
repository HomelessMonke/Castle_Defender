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
            towersInitializer.Init();
            allyDetectionArea.Init(128);
            
            allyArchersSpawner.SpawnAllUnits();
            allyMeleeSpawner.SpawnAllUnits();
            
            signalBus.Subscribe<FinishWaveSignal>(OnWaveFinished);
        }

        void OnWaveFinished()
        {
            castle.RestoreHealth();
            allyMeleeSpawner.RestoreUnits();
        }
    }
}