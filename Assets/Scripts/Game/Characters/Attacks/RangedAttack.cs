using System;

namespace Game.Characters.Attacks
{
    public class RangedAttack: IAttack
    {
        int attackPoints;
        public event Action AttackCompleted;
        
        public void Init(int attackPoints)
        {
            this.attackPoints = attackPoints;
        }
        
        public void Attack(HealthComponent targetHP)
        {
            /*
             * создаём стрелу
             * вызываём полёт
             * подписываем что должно произойти когда стрела долетит
             */
        }
    }
}