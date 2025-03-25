using Game.Characters.Spawners;
using UnityEngine;

namespace Game
{
    public class LevelEntryPoint: MonoBehaviour
    {
        [SerializeField]
        CharactersSpawner charactersSpawner;

        [SerializeField]
        WavesManager wavesManager;
        
        public void Start()
        {
            charactersSpawner.Init();
            wavesManager.LaunchWaves();
        }
    }
}