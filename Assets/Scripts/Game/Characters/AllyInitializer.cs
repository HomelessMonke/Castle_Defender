using UnityEngine;

namespace Game.Characters
{
    public class AllyInitializer: MonoBehaviour
    {
        [SerializeField]
        TowersInitializer towersInitializer;
        
        public void Init()
        {
            towersInitializer.Init();
        }
    }
}