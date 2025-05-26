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
        UIHealthView castleHpView;

        [SerializeField]
        TargetsDetectionArea allyDetectionArea;
        
        [SerializeField]
        CharacterSpawnerList spawnManager;

        [SerializeField]
        AllyInitializer allyInitializer;
        
        [SerializeField]
        WavesSpawner wavesManager;
        
        [SerializeField]
        UIEntryPoint guiEntryPoint;
        
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