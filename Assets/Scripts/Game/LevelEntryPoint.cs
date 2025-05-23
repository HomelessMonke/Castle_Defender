using Game.Characters;
using Game.Characters.Spawners;
using Game.Upgrades;
using UI;
using UI.Abilities;
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
        GUIEntryPoint guiEntryPoint;
        
        public void Start()
        {
            castle.Init();
            guiEntryPoint.Init();
            spawnManager.Init();
            allyInitializer.Init();
            wavesManager.LaunchWaves();
            
            allyDetectionArea.Init(128);
        }
    }
}