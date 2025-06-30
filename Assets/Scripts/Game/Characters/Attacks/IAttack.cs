using Game.Characters.Units;

namespace Game.Characters.Attacks
{
    public interface IAttack
    {
        void Attack(float damage, IDamageable target);
    }
}