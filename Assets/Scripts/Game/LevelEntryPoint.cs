using Game.Characters;
using Game.Characters.Spawners;
using Game.UI;
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
        UIEntryPoint uiEntryPoint;
        
        public void Start()
        {
            castle.Init();
            uiEntryPoint.Init();
            spawnManager.Init();
            allyInitializer.Init();
            
            allyDetectionArea.Init(128);
        }
    }
}