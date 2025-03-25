using UnityEngine;
using Utilities.Attributes;

namespace Game.Characters.Spawners
{

    public class EnemySwordsManSpawner: CharacterSpawner
    {
        [SerializeField]
        ObjectsPool<SwordsMan> pool;

        [SerializeField]
        Transform spawnPointTransform;
        
        [SerializeField]
        Transform mainTargetToMove;
        
        public override void Init()
        {
            pool.Init();
        }

        public override Character[] Spawn(CharacterParameters parameters, int count)
        {
            var characters = new Character[count];
            for (int i = 0; i < count; i++)
            {
                characters[i] = Spawn(parameters);
            }
            return characters;
        }

        Character Spawn(CharacterParameters parameters)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPointTransform.position;
            unit.Init(mainTargetToMove, parameters);
            unit.Health.Died += ()=> pool.Despawn(unit);
            return unit;
        }
    }
}