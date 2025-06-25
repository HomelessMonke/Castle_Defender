using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters
{
    public class Health: MonoBehaviour
    {
        float currentHp;
        bool isImmortal;
        
        public float MaxHp { get; private set; }
        public float CurrentHp => currentHp; 
        public float Percentage => currentHp / MaxHp;
        public bool IsAlive => currentHp > 0;
        
        public event UnityAction Healed, Died;
        public event UnityAction<float> DamageTaken;

        public void Init(float maxHealth, bool isImmortal = false)
        {
            ResetEvents();
            SetHealth(maxHealth);
            this.isImmortal = isImmortal;
        }

        public void SetHealth(float maxHealth)
        {
            MaxHp = maxHealth;
            currentHp = maxHealth;
        }

        public void RestoreHealth()
        {
            currentHp = MaxHp;
        }

        public void GetDamage(float amount)
        {
            if (isImmortal)
            {
                DamageTaken?.Invoke(amount);
                return;
            }
            
            currentHp = Mathf.Max(currentHp-amount,0);
            DamageTaken?.Invoke(amount);
            
            if (currentHp <= 0)
                Died?.Invoke();
        }

        public void GetHeal(float amount)
        {
            currentHp += amount;
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
