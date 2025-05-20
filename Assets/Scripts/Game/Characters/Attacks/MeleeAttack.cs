using System;

namespace Game.Characters.Attacks
{
    public class MeleeAttack: IAttack
    {
        public void Attack(int damage, Health targetHP)
        {
            targetHP.GetDamage(damage);
        }
    }
}