using UnityEngine;

namespace Game.Characters.States
{
    public class AnimatedIdleState: IState
    {
        Animator animator;
        
        public bool CanSelfEnter => false;

        public AnimatedIdleState(Animator animator)
        {
            this.animator = animator;
        }

        public void Enter()
        {
            animator.SetTrigger("Idle");
        }
        
        public void Update()
        {
            
        }
        
        public void Exit()
        {
            
        }
    }

}