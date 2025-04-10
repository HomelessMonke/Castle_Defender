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
        CharactersSpawnerManager charactersSpawner;

        [SerializeField]
        WavesManager wavesManager;
        
        [SerializeField]
        PlayerAbilitiesUI playerAbilitiesUI;
        
        public void Start()
        {
            //TODO: убрать магию
            gates.Init(100);
            playerAbilitiesUI.Init();
            charactersSpawner.Init();
            wavesManager.LaunchWaves();
        }
    }
}