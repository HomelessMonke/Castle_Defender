using Game.Characters.Attacks;
using Game.Characters.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters.States
{
    public class AnimatedAttackState: IState
    {
        float damage;
        Animator animator;
        IAttack attackVariation;
        
        IDamageable target;
        string targetID;
        
        bool TargetMissed => !target.IsAlive || !targetID.Equals(target.Id);
        public bool CanSelfEnter => false;
        
        public event UnityAction LoseTargetToAttack;
        
        public AnimatedAttackState(IAttack attackVariation, CharacterAnimator characterAnimator)
        {
            this.attackVariation = attackVariation;
            characterAnimator.AttackEvent -= Attack;
            characterAnimator.AttackEvent += Attack;
            animator = characterAnimator.Animator;
        }

        public void Init(float damage)
        {
            this.damage = damage;
        }

        public void SetTarget(IDamageable target)
        {
            this.target = target;
            targetID = target.Id;
        }

        public void Enter()
        {
            if(animator)
                animator.SetTrigger("Attack");
        }
        
        public void Update()
        {
            if (TargetMissed)
                LoseTargetToAttack?.Invoke();
        }
        
        public void Exit()
        {
        }

        void Attack()
        {
            if (TargetMissed)
                LoseTargetToAttack?.Invoke();
            
            attackVariation.Attack(damage, target);
        }
    }
}