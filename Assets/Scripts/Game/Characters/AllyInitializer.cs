using UnityEngine;

namespace Game.Characters
{
    public class AllyInitializer: MonoBehaviour
    {
        [SerializeField]
        AllyArchersSpawner allyArchersSpawner;
        
        [SerializeField]
        TowersInitializer towersInitializer;
        
        public void Init()
        {
            towersInitializer.Init();
            allyArchersSpawner.SpawnAllyArchers();
        }
    }
}