using UnityEngine;

namespace Game.Characters
{
    public abstract class Character: MonoBehaviour
    {
        [SerializeField]
        protected HealthComponent health;
        
        public HealthComponent Health => health;
    }
}