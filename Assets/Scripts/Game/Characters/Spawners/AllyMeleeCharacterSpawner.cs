using Game.Characters.Parameters;
using Game.Characters.Units;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public class AllyMeleeCharacterSpawner : AllySpawner<AllyMeleeCharacter>
    {
        [Space(20)]
        [SerializeField]
        AllyMeleeParameters parameters;

        public override void Init()
        {
            pool.Init(parameters.Count);
        }
        public override void SpawnAllUnits()
        {
            var positions = GetSpawnPoints(parameters.Count, parameters.MaxInLineCount);
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
    }
}