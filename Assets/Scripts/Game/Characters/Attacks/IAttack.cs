using System;

namespace Game.Characters.Attacks
{
    public interface IAttack
    {
        void Attack(float damage, Health targetHP);
    }
}