using System;
using System.Linq;
using Game.Characters.Parameters;
using Game.Characters.Units;
using Game.Signals.AllyMelee;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public class AllyMeleeCharacterSpawner : AllySpawner<AllyMeleeCharacter>
    {
        [Space(20)]
        [SerializeField]
        AllyMeleeParameters parameters;

        void Start()
        {
            signalBus.Subscribe<AllyMeleeHealthUpgradeSignal>(OnHealthPointsIncreased);
            signalBus.Subscribe<AllyMeleeCountUpgradeSignal>(OnArchersCountIncreased);
            signalBus.Subscribe<AllyMeleeDamageUpgradeSignal>(OnDamageIncreased);
        }

        public override void Init()
        {
            pool.Init(parameters.MeleeCount);
        }
        
        public override void SpawnAllUnits()
        {
            var positions = GetSpawnPoints(parameters.MeleeCount, parameters.MaxInLineCount);
            SpawnUnitsAtPositions(positions);
        }
        
        protected override void SpawnUnit(Vector2 spawnPos)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPos;
            unit.Init(parameters);
            unit.Died += () => OnUnitDied(unit);
            units.Add(unit);
        }

        void OnUnitDied(AllyMeleeCharacter unit)
        {
            pool.Despawn(unit);
            units.Remove(unit);
        }
        
        void OnArchersCountIncreased(AllyMeleeCountUpgradeSignal signal)
        {
            var positions = GetSpawnPoints(parameters.MeleeCount, parameters.MaxInLineCount);
            UpdateUnitsPositions(positions);
            var newArchersPositions = positions.TakeLast(signal.AddCount);
            SpawnUnitsAtPositions(newArchersPositions);
        }
        
        void OnDamageIncreased()
        {
            var damage = parameters.Damage;
            Debug.Log($"AllyMeleeDamage = {damage}");
            foreach (var unit in units)
            {
                unit.SetAttackParameter(damage);
            }
        }
        
        void OnHealthPointsIncreased()
        {
            var hp = parameters.HealthPoints;
            Debug.Log($"AllyMeleeHealth = {hp}");
            foreach (var unit in units)
            {
                unit.SetHealthPoints(hp);
            }
        }
    }
}