using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.States
{
    public class MoveState : IState
    { 
        NavMeshAgent agent;
        Transform targetObj;
        Vector2 targetPos;
        bool isStaticTarget;
        Vector2 lastPos;
        
        bool IsArrivedToTarget => agent.hasPath && agent.remainingDistance <= agent.stoppingDistance;
        public bool CanSelfEnter => true;
        
        public event UnityAction<Transform> ArrivedToTarget;
        
        public MoveState(NavMeshAgent agent)
        {
            this.agent = agent;
        }

        public void Init(float moveSpeed, float attackDistance)
        {
            agent.speed = moveSpeed;
            agent.stoppingDistance = attackDistance;
        }

        public void SetTargetObj(Transform target)
        {
            agent.ResetPath();
            targetObj = target;
        }

        public void SetTargetPos(Vector2 position)
        {
            agent.ResetPath();
            targetObj = null;
            targetPos = position;
        }

        public void Enter()
        {
            if(!targetObj)
                agent.SetDestination(targetPos);
        }
        
        public void Update()
        {
            if (targetObj)
            {
                targetPos = targetObj.position;
                agent.SetDestination(targetPos);
            }
            else if(!agent.hasPath)
            {
                agent.SetDestination(targetPos);
            }
            
            if (IsArrivedToTarget)
            {
                ArrivedToTarget?.Invoke(targetObj);
            }
        }
        
        public void Exit()
        {
        }
    }
}