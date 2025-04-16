using System;
namespace Game.Characters.Attacks
{
    public class MeleeAttack: IAttack
    {
        int attackPoints;

        public event Action AttackCompleted;
        
        public void Init(int attackPoints)
        {
            this.attackPoints = attackPoints;
        }
        public void Attack(HealthComponent targetHP)
        {
            targetHP.GetDamage(attackPoints);
            AttackCompleted?.Invoke();
        }
    }
}