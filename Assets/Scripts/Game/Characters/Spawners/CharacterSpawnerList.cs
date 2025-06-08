using System.Linq;
using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Characters.Spawners
{
    public class CharacterSpawnerList: MonoBehaviour
    {
        [SerializeField]
        CharacterSpawner[] spawners;

        [SerializeField]
        ProjectileSpawner projectileSpawner;
        
        [SerializeField]
        LootBubbleSpawner lootBubbleSpawner;
        
        public void Init()
        {
            projectileSpawner.Init();
            lootBubbleSpawner.Init();
            foreach (var spawner in spawners)
            {
                spawner.Init();
            }
        }

        public Character[] Spawn(SquadInfo squadInfo)
        {
            var characterType = squadInfo.Type;
            var spawner = spawners.FirstOrDefault(x => x.CharacterType.Equals(characterType));
            if (spawner)
            {
                return spawner.Spawn(squadInfo);;
            }
            return null;
        }
        
#if UNITY_EDITOR
        [Button]
        void SetAllSpawners()
        {
            spawners = GetComponentsInChildren<CharacterSpawner>();
            EditorUtility.SetDirty(this);
            projectileSpawner = GetComponentInChildren<ProjectileSpawner>();
        }
#endif
    }
}