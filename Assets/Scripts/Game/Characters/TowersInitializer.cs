using Game.Characters.Spawners;
using Game.Characters.Units;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Characters
{
    public class TowersInitializer : MonoBehaviour
    {
        [SerializeField]
        ShootingTower[] towers;

        [SerializeField]
        ProjectileSpawner arrowForTowerSpawner;
        
        public void Init()
        {
            foreach (var tower in towers)
            {
                tower.Init(1, 10, 10, arrowForTowerSpawner);
            }                    
        }

        [Button]
        void GetTowersInChildren()
        {
            towers = transform.GetComponentsInChildren<ShootingTower>();
            EditorUtility.SetDirty(this);
        }
    }
}