using Game.Characters.Attacks;
using Game.Characters.Units;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Game.Characters.States
{
    public class AttackState: IState
    {
        int damage;
        IAttack attackVariation;
        Character character;
        CustomTimer timer;
        
        HealthComponent targetHP;

        public bool CanSelfEnter => false;
        
        public event UnityAction LoseTargetToAttack;
        
        public AttackState(IAttack attackVariation)
        {
            this.attackVariation = attackVariation;
            timer = new CustomTimer();
            timer.TimerEnd += Attack;
        }

        public void Init(int damage, float attackCD)
        {
            this.damage = damage;
            timer.SetDuration(attackCD);
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
            
            attackVariation.Attack(damage, targetHP);
            timer.Restart();
        }
    }
}