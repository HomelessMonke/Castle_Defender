using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters.States
{
    public class NoneTargetMoveState : IState
    {
        bool canSelfEnter;
        
        Transform unitTransform;
        Vector2 moveDirection;
        Animator animator;
        
        public bool CanSelfEnter => canSelfEnter;
        
        public NoneTargetMoveState(Transform unitTransform, Animator animator, bool canSelfEnter = false)
        {
            this.unitTransform = unitTransform;
            this.canSelfEnter = canSelfEnter;
            this.animator = animator;
        }

        public void Init(Vector2 moveDirection, bool isSyncMove)
        {
            this.moveDirection = moveDirection;
            if (!isSyncMove)
            {
                animator.SetFloat("WalkOffset", Random.Range(0f, 1f));
            }
        }

        public void Enter()
        {
            animator.SetTrigger("Walk");
        }
        
        public void Update()
        {
            unitTransform.Translate(moveDirection * Time.deltaTime);
        }
        
        public void Exit() { }
    }
}