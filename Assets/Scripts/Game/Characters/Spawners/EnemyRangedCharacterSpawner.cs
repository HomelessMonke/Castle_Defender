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
    public class EnemyRangedCharacterSpawner: EnemyCharacterSpawner
    {
        [SerializeField]
        ObjectsPool<EnemyRangedCharacter> pool;

        [SerializeField]
        EnemyRangedParameters unitParameters;
        
        [SerializeField]
        ProjectileSpawner projectileSpawner;
        
        [SerializeField]
        LootBubbleSpawner bubbleSpawner;

        [SerializeField]
        string idPattern = "EnemyMelee{0}";
        
        SignalBus signalBus;
        CurrencyManager currencyService;
        
        List<EnemyRangedCharacter> units = new();
        
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
            var isSyncMove = squadInfo.IsSyncMovement;
            for (int i = 0; i < count; i++)
            {
                characters[i] = Spawn(spawnPositions[i], isSyncMove);
            }
            return characters;
        }
        
        public override void DespawnAllUnits()
        {
            foreach (var unit in units)
            {
                pool.Despawn(unit);
            }
        }
        
        public override void SetAllUnitsIdleState()
        {
            foreach (var unit in units)
            {
                unit.Reset();
                unit.SetIdleState();
            }
        }
        
        Character Spawn(Vector2 spawnPosition, bool isSyncMove)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPosition;
            var id = string.Format(idPattern, pool.Counter);
            unit.Init(id, unitParameters, projectileSpawner, isSyncMove);
            unit.Died+= ()=> OnDie(unit);
            units.Add(unit);
            return unit;
        }

        void OnDie(EnemyRangedCharacter unit)
        {
            bubbleSpawner.Spawn(unit.transform.position, unitParameters.CoinReward);
            units.Remove(unit);
            pool.Despawn(unit);

            currencyService.Earn(CurrencyType.Soft, unitParameters.CoinReward);
            signalBus.Fire<DespawnEnemySignal>();
        }
    }
}