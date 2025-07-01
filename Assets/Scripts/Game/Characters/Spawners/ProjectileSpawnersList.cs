using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Characters.Spawners
{
    public class ProjectileSpawnersList : MonoBehaviour
    {
        [SerializeField]
        ProjectileSpawner[] projectileSpawners;

        public void Init()
        {
            foreach (var spawner in projectileSpawners)
                spawner.Init();
        }
        
#if UNITY_EDITOR
        [Button]
        void SetAllSpawners()
        {
            projectileSpawners = GetComponentsInChildren<ProjectileSpawner>();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}