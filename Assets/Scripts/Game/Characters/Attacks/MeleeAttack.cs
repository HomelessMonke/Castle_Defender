using Game.Characters.Units;

namespace Game.Characters.Attacks
{
    public class MeleeAttack: IAttack
    {
        public void Attack(float damage, IDamageable target)
        {
            target.HealthComponent.GetDamage(damage);
        }
    }
}