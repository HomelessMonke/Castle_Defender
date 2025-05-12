using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.States
{
    public class MoveState : IState
    { 
        NavMeshAgent agent;
        Transform agentTransform;
        Transform targetObj;
        bool canSelfEnter;
        Vector2 moveDirection;
        
        
        bool IsArrivedToTarget => agent.hasPath && agent.remainingDistance <= agent.stoppingDistance;
        public bool CanSelfEnter => canSelfEnter;
        
        public event UnityAction<Transform> ArrivedToTarget;
        
        public MoveState(NavMeshAgent agent, bool canSelfEnter = false)
        {
            this.agent = agent;
            agentTransform = agent.transform;
            this.canSelfEnter = canSelfEnter;
        }

        public void Init(Vector2 moveDirection, float attackDistance)
        {
            agent.speed = Mathf.Abs(moveDirection.x);
            this.moveDirection = moveDirection;
            agent.stoppingDistance = attackDistance;
        }

        public void SetTargetObj(Transform target)
        {
            if (!target)
            {
                agent.ResetPath();
                targetObj = null;
                return;
            }

            if (target.Equals(targetObj))
            {
                return;
            }
            
            agent.ResetPath();
            targetObj = target;
        }

        public void Enter()
        {
            agent.ResetPath();
        }
        
        public void Update()
        {
            if (targetObj)
            {
                agent.SetDestination(targetObj.position);
            }
            else
            {
                agentTransform.Translate(moveDirection * Time.deltaTime);
            }
            
            if (IsArrivedToTarget)
                ArrivedToTarget?.Invoke(targetObj);
        }
        
        public void Exit()
        {
            agent.ResetPath();
        }
    }
}