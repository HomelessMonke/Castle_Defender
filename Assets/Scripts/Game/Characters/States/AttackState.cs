using Game.Characters.Attacks;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Game.Characters.States
{
    public class AttackState: IState
    {
        IAttack attackVariation;
        Character character;
        CustomTimer timer;
        
        HealthComponent targetHP;

        public event UnityAction LoseTargetToAttack;
        
        public AttackState(IAttack attackVariation)
        {
            timer = new CustomTimer();
            timer.OnTimerEnd += Attack;
            this.attackVariation = attackVariation;
            attackVariation.AttackCompleted += timer.Restart;
        }

        public void Init(int attackPoints, float attackCD)
        {
            timer.SetDuration(attackCD);
            attackVariation.Init(attackPoints);
        }

        public void SetTarget(HealthComponent targetHP)
        {
            this.targetHP = targetHP;
        }

        public void Enter()
        {
            Attack();
            timer.Reset();
            timer.Start();
        }
        
        public void Update()
        {
            if (!targetHP.IsAlive)
            {
                LoseTargetToAttack?.Invoke();
                return;
            }
            
            timer.Tick(Time.deltaTime);
        }
        
        public void Exit()
        {
            timer.Stop();
        }
        
        void Attack()
        {
            if (!targetHP.IsAlive)
            {
                LoseTargetToAttack?.Invoke();
                return;
            }
            
            attackVariation.Attack(targetHP);
        }
    }
}