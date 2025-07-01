using Game.Characters.Attacks;
using Game.Characters.Units;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Game.Characters.States
{
    public class NotAnimatedAttackState: IState
    {
        int damage;
        IAttack attackVariation;
        Character character;
        CustomTimer timer;

        IDamageable target;
        string targetID;

        bool TargetMissed => !target.IsAlive || !targetID.Equals(target.Id);
        
        public bool CanSelfEnter => false;
        
        public event UnityAction LoseTargetToAttack;
        
        public NotAnimatedAttackState(IAttack attackVariation)
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

        public void SetTarget(IDamageable target)
        {
            this.target = target;
            targetID = target.Id;
        }

        public void Enter()
        {
            Attack();
            timer.Reset();
            timer.Start();
        }
        
        public void Update()
        {
            if (TargetMissed)
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
            if (TargetMissed)
            {
                LoseTargetToAttack?.Invoke();
                return;
            }
            
            attackVariation.Attack(damage, target);
            timer.Restart();
        }
    }
}