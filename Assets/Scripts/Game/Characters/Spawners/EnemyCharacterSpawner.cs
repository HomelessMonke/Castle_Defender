using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public abstract class EnemyCharacterSpawner: MonoBehaviour
    {
        [SerializeField]
        EnemyType enemyType;
        
        public EnemyType Enemy => enemyType;

        public abstract void Init();

        public abstract Character[] Spawn(SquadInfo squadInfo);
    }
}