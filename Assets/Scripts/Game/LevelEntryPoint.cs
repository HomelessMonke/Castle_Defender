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
        CharacterSpawnerList spawnManager;

        [SerializeField]
        AllyInitializer allyInitializer;
        
        [SerializeField]
        WavesSpawner wavesManager;
        
        [SerializeField]
        PlayerAbilitiesUI playerAbilitiesUI;
        
        public void Start()
        {
            castle.Init();
            playerAbilitiesUI.Init();
            spawnManager.Init();
            allyInitializer.Init();
            wavesManager.LaunchWaves();
            
            allyDetectionArea.Init(128);
        }
    }
}