using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.States
{
    public class MoveState : IState
    { 
        NavMeshAgent agent;
        Transform target;
        bool isStaticTarget;
        
        bool IsArrivedToTarget => agent.hasPath && agent.remainingDistance <= agent.stoppingDistance;
        
        public event UnityAction ArrivedToTarget;
        
        public MoveState(NavMeshAgent agent)
        {
            this.agent = agent;
        }

        public void Init(float moveSpeed, float attackDistance)
        {
            agent.speed = moveSpeed;
            agent.stoppingDistance = attackDistance;
        }

        public void SetTarget(Transform target, bool isStaticTarget)
        {
            this.target = target;
            this.isStaticTarget = isStaticTarget;
            agent.ResetPath();
        }

        public void Enter()
        {
            if(isStaticTarget)
                agent.SetDestination(target.position);
        }
        
        public void Update()
        {
            if(!isStaticTarget || !agent.hasPath)
                agent.SetDestination(target.position);
            
            if (IsArrivedToTarget)
            {
                ArrivedToTarget?.Invoke();
            }
        }
        
        public void Exit()
        {
            agent.ResetPath();
        }
    }
}