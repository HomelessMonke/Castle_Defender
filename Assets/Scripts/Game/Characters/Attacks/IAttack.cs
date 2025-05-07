using System;

namespace Game.Characters.Attacks
{
    public interface IAttack
    {
        public event Action AttackCompleted;
        
        void Attack(int damage, Health targetHP);
    }
}