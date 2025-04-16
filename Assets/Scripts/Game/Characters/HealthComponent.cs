using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters
{
    public class HealthComponent: MonoBehaviour
    {
        int currentHealth;
        int maxHealth;
        
        public float Percentage => (float)currentHealth / maxHealth;
        public bool IsAlive => currentHealth > 0;
        
        public event UnityAction DamageTaken, Healed, Died;

        public void Init(int maxHealth)
        {
            ResetEvents();
            this.maxHealth = maxHealth;
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
