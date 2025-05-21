using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters
{
    public class Health: MonoBehaviour
    {
        float currentHealth;
        bool isImmortal;
        
        public float MaxHealth { get; private set; } 
        public float CurrentHealth => currentHealth;
        public float Percentage => currentHealth / MaxHealth;
        public bool IsAlive => currentHealth > 0;
        
        public event UnityAction Healed, Died;
        public event UnityAction<float> DamageTaken;

        public void Init(float maxHealth, bool isImmortal = false)
        {
            ResetEvents();
            MaxHealth = maxHealth;
            currentHealth = maxHealth;
            this.isImmortal = isImmortal;
        }

        public void GetDamage(float amount)
        {
            if (isImmortal)
            {
                DamageTaken?.Invoke(amount);
                return;
            }
            
            currentHealth = Mathf.Max(currentHealth-amount,0);
            DamageTaken?.Invoke(amount);
            
            if (currentHealth <= 0)
                Died?.Invoke();
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
