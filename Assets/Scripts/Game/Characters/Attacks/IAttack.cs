using System;

namespace Game.Characters.Attacks
{
    public interface IAttack
    {
        public event Action AttackCompleted;
        
        public void Init(int attackPoints);
        
        void Attack(HealthComponent targetHP);
    }

}