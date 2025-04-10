using UnityEngine;
using UnityEngine.Events;
using Utilities.Attributes;

namespace Game.Characters
{
    public class HealthComponent: MonoBehaviour
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        
        public float Percentage => (float)CurrentHealth / MaxHealth;
        public bool IsAlive => CurrentHealth > 0;
        
        public event UnityAction DamageTaken, Healed, Died;

        public void Init(int maxHealth)
        {
            ResetEvents();
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        [Button]
        void GetDamage()
        {
            GetDamage(5);
        }

        [Button]
        public void GetHeal()
        {
            GetHeal(5);
        }

        public void GetDamage(int amount)
        {
            CurrentHealth = Mathf.Max(CurrentHealth-amount,0);
            DamageTaken?.Invoke();
            
            if (CurrentHealth <= 0)
            {
                Died?.Invoke();
            }
        }

        public void GetHeal(int amount)
        {
            CurrentHealth += amount;
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
