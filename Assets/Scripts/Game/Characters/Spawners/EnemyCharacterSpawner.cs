using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public abstract class EnemyCharacterSpawner: MonoBehaviour
    {
        [SerializeField]
        protected Transform spawnPointTransform;
        
        [SerializeField]
        EnemyType enemyType;
        
        public EnemyType EnemyType => enemyType;

        public abstract void Init();

        public abstract Character[] Spawn(SquadInfo squadInfo);
    }
}