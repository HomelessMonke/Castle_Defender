using System;
using UnityEngine;
using Utilities;

namespace Game.Characters.States
{
    public class AttackState: IState
    {
        Character character;
        CustomTimer timer;
        int attackPoints;
        
        public AttackState(Character character)
        {
            this.character = character;
            attackPoints = character.Parameters.AttackPoints;
            timer = new CustomTimer(character.Parameters.AttackCD);
            timer.OnTimerEnd += Attack;
        }

        public void Enter()
        {
            character.stateLog.Append("<color=brown>AttackState ENTER</color>");
            character.stateLog.Append(Environment.NewLine);
            Attack();
            timer.Reset();
            timer.Start();
        }
        
        public void Update()
        {
            if (!character.FovTarget)
            {
                character.SetMoveState();
                return;
            }
            
            timer.Tick(Time.deltaTime);
        }
        
        public void Exit()
        {
            character.stateLog.Append("<color=brown>AttackState EXIT</color>");
            character.stateLog.Append(Environment.NewLine);
            timer.Stop();
        }
        
        void Attack()
        {
            var targetHP = character.FovTarget.GetComponent<HealthComponent>();
            if (!targetHP || !targetHP.IsAlive)
            {
                character.SetMoveState();
                return;
            }
            targetHP.GetDamage(attackPoints);
            timer.Restart();
        }
    }
}