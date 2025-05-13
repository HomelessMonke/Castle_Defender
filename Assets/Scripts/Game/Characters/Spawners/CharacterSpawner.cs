using Game.Characters.Spawners.FormationSpawnParameters;
using Game.Characters.Units;
using Game.Waves;
using UnityEngine;

namespace Game.Characters.Spawners
{

    public abstract class CharacterSpawner: MonoBehaviour
    {
        [SerializeField]
        CharacterType characterType;
        
        public CharacterType CharacterType => characterType;

        public abstract void Init();

        public abstract Character[] Spawn(SquadInfo squadInfo);
    }
}