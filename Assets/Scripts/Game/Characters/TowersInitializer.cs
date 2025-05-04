using Game.Characters.Parameters;
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
        BallistaTower[] towers;

        [SerializeField]
        BallistaTowerParameters parameters;
        
        [SerializeField]
        ProjectileSpawner arrowForTowerSpawner;
        
        public void Init()
        {
            foreach (var tower in towers)
            {
                tower.Init(parameters, arrowForTowerSpawner);
            }                    
        }

        [Button]
        void GetTowersInChildren()
        {
            towers = transform.GetComponentsInChildren<BallistaTower>();
            EditorUtility.SetDirty(this);
        }
    }
}