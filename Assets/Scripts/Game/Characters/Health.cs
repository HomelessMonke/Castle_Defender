using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters
{
    public class Health: MonoBehaviour
    {
        public int MaxHealth { get; private set; } 
        int currentHealth;
        
        public float Percentage => (float)currentHealth / MaxHealth;
        public bool IsAlive => currentHealth > 0;
        
        public event UnityAction DamageTaken, Healed, Died;

        public void Init(int maxHealth)
        {
            ResetEvents();
            MaxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void GetDamage(int amount)
        {
            currentHealth = Mathf.Max(currentHealth-amount,0);
            DamageTaken?.Invoke();
            
            if (currentHealth <= 0)
            {
                Died?.Invoke();
            }
        }

        public void GetHeal(int amount)
        {
            currentHealth += amount;
            Healed?.Invoke();
        }
        
        void ResetEvents()
        {
            DamageTaken = null;
            Healed = null;
            Died = null;
        }
    }
}
