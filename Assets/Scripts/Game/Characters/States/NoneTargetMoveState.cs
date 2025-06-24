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

        public void Init(Vector2 moveDirection)
        {
            this.moveDirection = moveDirection;
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