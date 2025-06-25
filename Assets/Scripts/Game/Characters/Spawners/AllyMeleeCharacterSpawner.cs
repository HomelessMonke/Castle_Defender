using System;
using System.Linq;
using Game.Characters.Parameters;
using Game.Characters.Units;
using Game.Signals;
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
            signalBus.Subscribe<AllyMeleeCountUpgradeSignal>(OnUnitsCountIncreased);
            signalBus.Subscribe<AllyMeleeDamageUpgradeSignal>(OnDamageIncreased);
            signalBus.Subscribe<LaunchWaveSignal>(OnLaunchWave);
            signalBus.Subscribe<FinishWaveSignal>(OnFinishWave);
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

        public void RestoreUnits()
        {
            foreach (var unit in units)
            {
                unit.Reset();
                pool.Despawn(unit);
            }
            units.Clear();

            SpawnAllUnits();
        }

        protected override void SpawnUnit(Vector2 spawnPos)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPos;
            unit.Died += () => OnUnitDied(unit);
            unit.Init(parameters);
            units.Add(unit);
        }

        void OnUnitDied(AllyMeleeCharacter unit)
        {
            pool.Despawn(unit);
            units.Remove(unit);
        }
        
        void OnUnitsCountIncreased(AllyMeleeCountUpgradeSignal signal)
        {
            var positions = GetSpawnPoints(parameters.MeleeCount, parameters.MaxInLineCount);
            SetPositionsForUnits(positions);
            var newUnitsPositions = positions.TakeLast(signal.AddCount);
            SpawnUnitsAtPositions(newUnitsPositions);
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

        void OnLaunchWave()
        {
            foreach (var unit in units)
            {
                unit.SetImmortal(false);
            }
        }

        void OnFinishWave()
        {
            foreach (var unit in units)
            {
                unit.SetImmortal(true);
            }
        }
    }
}