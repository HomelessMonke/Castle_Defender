using Game.Characters;
using Game.Characters.Spawners;
using Game.Signals;
using Game.UI;
using Game.Waves;
using UnityEngine;
using Zenject;

namespace Game
{
    public class LevelEntryPoint: MonoBehaviour
    {
        [SerializeField]
        EnemySpawnersList enemySpawnersList;

        [SerializeField]
        AlliesInitializer alliesInitializer;
        
        [SerializeField]
        ProjectileSpawnersList projectileSpawnersList;
        
        [SerializeField]
        WavesList wavesList;
        
        [SerializeField]
        UIEntryPoint uiEntryPoint;

        
        public void Start()
        {
            uiEntryPoint.Init();
            
            projectileSpawnersList.Init();
            alliesInitializer.Init();
            enemySpawnersList.Init();
        }
    }
}