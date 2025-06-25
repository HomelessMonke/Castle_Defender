 using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.States
{
    public class TargetMoveState : IState
    {
        Animator animator;
        NavMeshAgent agent;
        Transform targetObj;
        bool canSelfEnter;
        int updatePathPerFrame;

        int currentFrameNumber;
        
        bool IsArrivedToTarget => agent.hasPath && agent.remainingDistance <= agent.stoppingDistance;
        public bool CanSelfEnter => canSelfEnter;
        
        public event UnityAction<Transform> ArrivedToTarget;
        
        public TargetMoveState(NavMeshAgent agent, Animator animator, int updatePathPerFrame, bool canSelfEnter = false)
        {
            this.agent = agent;
            this.animator = animator;
            this.canSelfEnter = canSelfEnter;
            this.updatePathPerFrame = updatePathPerFrame;
        }

        public void Init(float speed, float stoppingDistance)
        {
            agent.speed = speed;
            agent.stoppingDistance = stoppingDistance;
        }

        public void SwitchTarget(Transform target)
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
            agent.SetDestination(targetObj.position);
        }

        public void Enter()
        {
            animator.SetTrigger("Walk");
        }
        
        public void Update()
        {
            currentFrameNumber++;
            if (currentFrameNumber >= updatePathPerFrame)
            {
                agent.SetDestination(targetObj.position);
                currentFrameNumber = 0;
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