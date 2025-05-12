using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters
{
    public class Health: MonoBehaviour
    {
        int currentHealth;
        bool isImmortal;
        
        public int MaxHealth { get; private set; } 
        public float Percentage => (float)currentHealth / MaxHealth;
        public bool IsAlive => currentHealth > 0;
        
        public event UnityAction Healed, Died;
        public event UnityAction<int> DamageTaken;

        public void Init(int maxHealth, bool isImmortal = false)
        {
            ResetEvents();
            MaxHealth = maxHealth;
            currentHealth = maxHealth;
            this.isImmortal = isImmortal;
        }

        public void GetDamage(int amount)
        {
            DamageTaken?.Invoke(amount);
            if(isImmortal)
                return;
            
            currentHealth = Mathf.Max(currentHealth-amount,0);
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
