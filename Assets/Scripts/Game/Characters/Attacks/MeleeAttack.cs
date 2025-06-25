namespace Game.Characters.Attacks
{
    public class MeleeAttack: IAttack
    {
        public void Attack(float damage, Health targetHP)
        {
            targetHP.GetDamage(damage);
        }
    }
}