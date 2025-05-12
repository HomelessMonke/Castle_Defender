using Game.Characters;
using Game.Characters.Spawners;
using UI;
using UnityEngine;

namespace Game
{
    public class LevelEntryPoint: MonoBehaviour
    {
        [SerializeField]
        Castle castle;

        [SerializeField]
        TargetsDetectionArea allyDetectionArea;
        
        [SerializeField]
        TargetsDetectionArea enemyDetectionArea;
        
        [SerializeField]
        CharacterSpawnerList spawnManager;

        [SerializeField]
        AllyInitializer allyInitializer;
        
        [SerializeField]
        WavesSpawner wavesManager;
        
        [SerializeField]
        PlayerAbilitiesUI playerAbilitiesUI;
        
        public void Start()
        {
            castle.Init(100);
            playerAbilitiesUI.Init();
            spawnManager.Init();
            allyInitializer.Init();
            wavesManager.LaunchWaves();
            
            allyDetectionArea.Init(16);
            enemyDetectionArea.Init(128);
            enemyDetectionArea.RegisterTargets(castle.HpAreas);
        }
    }
}