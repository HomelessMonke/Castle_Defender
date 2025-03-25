using UnityEngine;

namespace Game.Characters
{
    public class Gates : MonoBehaviour
    {
        [SerializeField]
        HealthComponent healthComponent;
        
        void Init(int maxHealth)
        {
            healthComponent.Init(maxHealth);
        }
    }
}