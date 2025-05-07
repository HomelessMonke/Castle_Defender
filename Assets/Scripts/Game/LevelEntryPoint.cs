using Game.Characters;
using Game.Characters.Spawners;
using UI;
using UnityEngine;

namespace Game
{
    public class LevelEntryPoint: MonoBehaviour
    {
        [SerializeField]
        Gates gates;
        
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
            //TODO: убрать магию
            gates.Init(100);
            playerAbilitiesUI.Init();
            spawnManager.Init();
            allyInitializer.Init();
            wavesManager.LaunchWaves();
        }
    }
}