using Game.Characters.Spawners;
using UnityEngine;

namespace Game.Characters
{
    public class AllyInitializer: MonoBehaviour
    {
        [SerializeField]
        AllyArchersSpawner allyArchersSpawner;
        
        [SerializeField]
        AllyMeleeCharacterSpawner allyMeleeSpawner;
        
        [SerializeField]
        TowersInitializer towersInitializer;
        
        public void Init()
        {
            towersInitializer.Init();
            allyArchersSpawner.SpawnAllUnits();
            allyMeleeSpawner.SpawnAllUnits();
        }
    }
}