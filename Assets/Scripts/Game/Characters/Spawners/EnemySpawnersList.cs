using System.Linq;
using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Characters.Spawners
{
    public class EnemySpawnersList: MonoBehaviour
    {
        [SerializeField]
        EnemyCharacterSpawner[] characterSpawners;
        
        [SerializeField]
        LootBubbleSpawner lootBubbleSpawner;
        
        public void Init()
        {
            lootBubbleSpawner.Init();
            
            foreach (var spawner in characterSpawners)
                spawner.Init();
        }

        public Character[] Spawn(SquadInfo squadInfo)
        {
            var characterType = squadInfo.Type;
            var spawner = characterSpawners.FirstOrDefault(x => x.EnemyType.Equals(characterType));
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
            characterSpawners = GetComponentsInChildren<EnemyCharacterSpawner>();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}