using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.States
{
    public class MoveState : IState
    {
        Character character;
        NavMeshAgent agent;
        Transform target;
        bool isStaticTarget;
        
        bool ArrivedToTarget => agent.hasPath && agent.remainingDistance <= agent.stoppingDistance;
        
        public MoveState(Character character)
        {
            this.character = character;
            agent = character.NavAgent;
            agent.speed = character.Parameters.MoveSpeed;
            agent.stoppingDistance = character.Parameters.AttackDistance;
        }

        public void SetTarget(Transform target, bool isStaticTarget)
        {
            this.target = target;
            this.isStaticTarget = isStaticTarget;
            agent.ResetPath();
        }

        public void Enter()
        {
            character.stateLog.Append("<color=magenta>MoveState ENTER</color>");
            character.stateLog.Append(Environment.NewLine);
            if(isStaticTarget)
                agent.SetDestination(target.position);
        }
        
        public void Update()
        {
            if(!isStaticTarget || !agent.hasPath)
                agent.SetDestination(target.position);
            
            Debug.DrawLine(character.transform.position, agent.destination, Color.red);
            if (ArrivedToTarget)
            {
                character.stateLog.Append("Вызов character.SetAttackState(target) из MoveState");
                character.stateLog.Append(Environment.NewLine);
                character.SetAttackState();
            }
        }
        
        public void Exit()
        {
            character.stateLog.Append("<color=magenta>MoveState EXIT</color>");
            character.stateLog.Append(Environment.NewLine);
            agent.ResetPath();
        }
    }
}