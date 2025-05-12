using Game.Characters.Attacks;
using Game.Characters.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters.States
{
    public class AnimatedAttackState: IState
    {
        int damage;
        Animator animator;
        IAttack attackVariation;
        Character character;
        
        Health targetHP;

        public bool CanSelfEnter => false;
        
        public event UnityAction LoseTargetToAttack;
        
        public AnimatedAttackState(IAttack attackVariation, CharacterAnimator characterAnimator)
        {
            this.attackVariation = attackVariation;
            characterAnimator.AttackEvent -= Attack;
            characterAnimator.AttackEvent += Attack;
            animator = characterAnimator.Animator;
        }

        public void Init(int damage, float attackCD)
        {
            this.damage = damage;
        }

        public void SetTarget(Health targetHP)
        {
            this.targetHP = targetHP;
        }

        public void Enter()
        {
            if(animator)
                animator.SetTrigger("Attack");
        }
        
        public void Update()
        {
            if (!targetHP.IsAlive)
                LoseTargetToAttack?.Invoke();
        }
        
        public void Exit()
        {
        }

        void Attack()
        {
            attackVariation.Attack(damage, targetHP);
        }
    }
}