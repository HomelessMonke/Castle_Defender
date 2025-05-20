using System;

namespace Game.Characters.Attacks
{
    public interface IAttack
    {
        void Attack(int damage, Health targetHP);
    }
}