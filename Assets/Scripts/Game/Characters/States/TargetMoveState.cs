 using Game.Characters.Units;
 using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.States
{
    public class TargetMoveState : IState
    {
        Animator animator;
        NavMeshAgent agent;

        string targetID;
        IDamageable target;

        int updatePathPerFrame;
        int currentFrameNumber;
        
        bool TargetMissed => !target.IsAlive || !targetID.Equals(target.Id);
        bool IsArrivedToTarget => agent.hasPath && agent.remainingDistance <= agent.stoppingDistance;
        public bool CanSelfEnter { get; }

        public event UnityAction LoseTarget;
        public event UnityAction<IDamageable> ArrivedToTarget;
        
        public TargetMoveState(NavMeshAgent agent, Animator animator, int updatePathPerFrame, bool canSelfEnter = false)
        {
            this.agent = agent;
            this.animator = animator;
            CanSelfEnter = canSelfEnter;
            this.updatePathPerFrame = updatePathPerFrame;
        }

        public void Init(float speed, float stoppingDistance)
        {
            agent.speed = speed;
            agent.stoppingDistance = stoppingDistance;
        }

        public void SwitchTarget(IDamageable targetObj)
        {
            if (targetObj == null)
            {
                ResetTarget();
                return;
            }

            if (targetObj.Equals(target))
                return;
            
            agent.ResetPath();
            target = targetObj;
            targetID = targetObj.Id;
            agent.SetDestination(target.Transform.position);
        }

        public void Enter()
        {
            animator.SetTrigger("Walk");
        }
        
        public void Update()
        {
            if (TargetMissed)
            {
                LoseTarget?.Invoke();
                return;
            }
            
            currentFrameNumber++;
            if (currentFrameNumber >= updatePathPerFrame)
            {
                agent.SetDestination(target.Transform.position);
                currentFrameNumber = 0;
            }
            
            if (IsArrivedToTarget)
                ArrivedToTarget?.Invoke(target);
        }

        void ResetTarget()
        {
            agent.ResetPath();
            target = null;
            targetID = null;
        }
        
        public void Exit()
        {
            if(agent.isActiveAndEnabled)
                agent.ResetPath();
        }
    }
}