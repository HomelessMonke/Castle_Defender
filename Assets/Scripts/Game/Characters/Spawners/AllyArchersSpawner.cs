using System.Linq;
using Game.Characters.Parameters;
using Game.Characters.Units;
using Game.Signals.AllyArcher;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public class AllyArchersSpawner : AllySpawner<AllyArcher>
    {
        [Space(20)]
        [SerializeField]
        AllyArchersParameters parameters;

        [SerializeField]
        ProjectileSpawner arrowsSpawner;

        public int ArchersCount => parameters.ArchersCount;
        
        public void Start()
        {
            signalBus.Subscribe<AllyArchersCountUpgradeSignal>(OnArchersCountIncreased);
            signalBus.Subscribe<AllyArchersDamageUpgradeSignal>(OnArchersDamageIncreased);
        }

        public override void Init()
        {
            pool.Init(parameters.ArchersCount);
        }
        
        public override void SpawnAllUnits()
        {
            var positions = GetSpawnPoints(parameters.ArchersCount, parameters.MaxInLine);
            SpawnUnitsAtPositions(positions);
        }

        protected override void SpawnUnit(Vector2 spawnPos)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPos;
            unit.Init(parameters, arrowsSpawner);
            units.Add(unit);
        }

        void OnArchersCountIncreased(AllyArchersCountUpgradeSignal signal)
        {
            var positions = GetSpawnPoints(parameters.ArchersCount, parameters.MaxInLine);
            SetPositionsForUnits(positions);
            var newArchersPositions = positions.TakeLast(signal.AddCount);
            SpawnUnitsAtPositions(newArchersPositions);
        }
        
        void OnArchersDamageIncreased(AllyArchersDamageUpgradeSignal signal)
        {
            var damage = parameters.Damage;
            Debug.Log($"AllyArchersDamage = {damage}");
            foreach (var unit in units)
            {
                unit.SetAttackParameter(damage);
            }
        }
    }
}