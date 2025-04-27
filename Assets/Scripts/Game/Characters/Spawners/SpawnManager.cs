using System.Linq;
using Game.Characters.Units;
using Game.Waves;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Characters.Spawners
{
    public class SpawnManager: MonoBehaviour
    {
        [SerializeField]
        CharacterSpawner[] spawners;

        [SerializeField]
        ProjectileSpawner projectileSpawner;
        
        public void Init()
        {
            projectileSpawner.Init();
            foreach (var spawner in spawners)
            {
                spawner.Init();
            }
        }

        public Character[] Spawn(SquadInfo squadInfo)
        {
            var characterType = squadInfo.Type;
            var charactersCount = squadInfo.Count;
            Character[] characters = new Character[charactersCount];
            var spawner = spawners.FirstOrDefault(x => x.CharacterType.Equals(characterType));
            if (spawner)
            {
                characters = spawner.Spawn(squadInfo);
                return characters;
            }
            return null;
        }

        [Button]
        void SetAllSpawners()
        {
            spawners = GetComponentsInChildren<CharacterSpawner>();
            EditorUtility.SetDirty(this);
            projectileSpawner = GetComponentInChildren<ProjectileSpawner>();
        }
    }
}