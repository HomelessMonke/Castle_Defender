using Game.Characters.Parameters;
using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public class AllyMeleeCharacterSpawner : CharacterSpawner
    {
        [SerializeField]
        ObjectsPool<MeleeCharacter> pool;
        
        [SerializeField]
        AllyMeleeUnitParameters unitParameters;
        
        [Space(10)]
        [SerializeField]
        Transform spawnPointTransform;
        
        public override void Init()
        {
            pool.Init();
        }
        
        public override Character[] Spawn(SquadInfo squadInfo)
        {
            var spawnPositions = squadInfo.GetSpawnPoints(spawnPointTransform);
            var count = spawnPositions.Length;
            var characters = new Character[count];
            for (int i = 0; i < count; i++)
            {
                characters[i] = Spawn(spawnPositions[i]);
            }
            return characters;
        }
        
        Character Spawn(Vector2 spawnPosition)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPosition;
            unit.Init(unitParameters);
            unit.Died+= ()=> pool.Despawn(unit);
            return unit;
        }
    }
}