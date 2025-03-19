using UnityEngine.Events;

namespace UnitComponents
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        
        public event UnityAction<int> DamageTaken, Healed;

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void Damage(int amount)
        {
            CurrentHealth -= amount;
            DamageTaken?.Invoke(amount);
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            Healed?.Invoke(amount);
        }
    }
}
