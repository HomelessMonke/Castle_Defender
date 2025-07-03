using System;
using System.Linq;
using Game.Currencies;
using Game.Signals;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;
using Zenject;

namespace Game.Characters.Spawners
{
    public class EnemySpawnersList: MonoBehaviour
    {
        [SerializeField]
        EnemyCharacterSpawner[] enemySpawners;
        
        [SerializeField]
        LootBubbleSpawner lootBubbleSpawner;
        
        SignalBus signalBus;
        CurrencyManager currencyManager;
        
        public EnemyCharacterSpawner GetSpawnerByType(EnemyType type) => enemySpawners.FirstOrDefault(x => x.EnemyType.Equals(type));
        
        [Inject]
        public void Construct(CurrencyManager currencyManager, SignalBus signalBus)
        {
            this.currencyManager = currencyManager;
            this.signalBus = signalBus;
        }

        void Start()
        {
            signalBus.Subscribe<WaveFinishedSignal>(OnWaveFinished);
            signalBus.Subscribe<ResetGameBoard>(OnResetGameBoard);
        }

        public void Init()
        {
            lootBubbleSpawner.Init();
            
            foreach (var spawner in enemySpawners)
                spawner.Init(currencyManager, signalBus);
        }

        void OnWaveFinished()
        {
            foreach (var spawner in enemySpawners)
            {
                spawner.SetAllUnitsIdleState();
            }
        }
        
        void OnResetGameBoard()
        {
            foreach (var spawner in enemySpawners)
                spawner.DespawnAllUnits();
        }
        
#if UNITY_EDITOR
        [Button]
        void SetAllSpawners()
        {
            enemySpawners = GetComponentsInChildren<EnemyCharacterSpawner>();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}