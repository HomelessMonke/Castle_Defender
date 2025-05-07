using UnityEngine;
using UnityEngine.Events;
namespace Game.Characters.States
{
    public class AimAttackState : IState
    {
        int currentFrame;
        int updatePerFrame = 5;
        float attackDistance;
        Transform parent;
        Health target;

        bool CanAttack => Vector2.Distance(target.transform.position, parent.position) <= attackDistance;
        public bool CanSelfEnter => false;
        
        public event UnityAction<Health> AttackTarget;

        public AimAttackState(Transform parent)
        {
            this.parent = parent;
        }

        public void SetTarget(Health target, float attackDistance)
        {
            this.attackDistance = attackDistance;
            this.target = target;
        }

        public void Enter()
        {
           
        }
        
        public void Update()
        {
            currentFrame++;
            if (currentFrame % updatePerFrame == 0)
            {
                currentFrame = 0;
                if (CanAttack)
                {
                    AttackTarget?.Invoke(target);
                }
            }
        }
        
        public void Exit()
        {
            
        }
    }
}