using System.Collections.Generic;
using Game.Characters.Parameters;
using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using Game.Currencies;
using Game.Signals;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public class EnemyMeleeCharacterSpawner: EnemyCharacterSpawner
    {
        [SerializeField]
        protected ObjectsPool<EnemyMeleeCharacter> pool;
        
        [SerializeField]
        EnemyMeleeParameters unitParameters;
        
        [SerializeField]
        LootBubbleSpawner bubbleSpawner;

        SignalBus signalBus;
        CurrencyManager currencyService;
        
        [SerializeField]
        string idPattern = "EnemyMelee{0}";
        
        List<EnemyMeleeCharacter> units = new();

        public override void Init(CurrencyManager currencyService, SignalBus signalBus)
        {
            this.currencyService = currencyService;
            this.signalBus = signalBus;
            pool.Init();
        }

        public override Character[] Spawn(SquadInfo squadInfo)
        {
            units.Clear();
            var spawnPositions = squadInfo.GetSpawnPoints(spawnPointTransform);
            var count = spawnPositions.Length;
            var characters = new Character[count];
            for (int i = 0; i < count; i++)
            {
                characters[i] = Spawn(spawnPositions[i]);
            }
            return characters;
        }
        public override void DespawnAllUnits()
        {
            foreach (var unit in units)
            {
                unit.Reset();
                pool.Despawn(unit);
            }
        }
        
        public override void SetAllUnitsIdleState()
        {
            foreach (var unit in units)
            {
                unit.SetIdleState();
            }
        }

        Character Spawn(Vector2 spawnPosition)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPosition;
            var id = string.Format(idPattern, pool.Counter);
            unit.Init(id, unitParameters);
            unit.Died+= ()=> OnDie(unit);
            units.Add(unit);
            return unit;
        }

        void OnDie(EnemyMeleeCharacter unit)
        {
            bubbleSpawner.Spawn(unit.transform.position, unitParameters.CoinReward);
            units.Remove(unit);
            pool.Despawn(unit);
            
            currencyService.Earn(CurrencyType.Soft, unitParameters.CoinReward);
            Debug.Log($"Die {unit.name}");
            signalBus.Fire<DespawnEnemySignal>();
        }
    }
}