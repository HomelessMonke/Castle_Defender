using System;

namespace Game.Characters.Attacks
{
    public class MeleeAttack: IAttack
    {
        public event Action AttackCompleted;
        
        public void Attack(int damage, HealthComponent targetHP)
        {
            targetHP.GetDamage(damage);
            AttackCompleted?.Invoke();
        }
    }
}