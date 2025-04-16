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
        int attackPoints;
        CharacterFieldOfView fieldOfView;

        public event UnityAction LoseTargetToAttack;
        
        public AttackState(CharacterFieldOfView fieldOfView)
        {
            // attacker = ;
            timer = new CustomTimer();
            timer.OnTimerEnd += Attack;
            this.fieldOfView = fieldOfView;
        }

        public void Init(int attackPoints, float attackCD)
        {
            timer.SetDuration(attackCD);
            this.attackPoints = attackPoints;
        }
        

        public void Enter()
        {
            Attack();
            timer.Reset();
            timer.Start();
        }
        
        public void Update()
        {
            if (!fieldOfView.CurrentTarget)
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
            var targetHP = fieldOfView.CurrentTarget.GetComponent<HealthComponent>();
            if (!targetHP || !targetHP.IsAlive)
            {
                LoseTargetToAttack?.Invoke();
                return;
            }
            targetHP.GetDamage(attackPoints);
            timer.Restart();
        }
    }
}