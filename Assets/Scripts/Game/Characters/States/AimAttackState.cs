using UnityEngine;
using UnityEngine.Events;
namespace Game.Characters.States
{
    public class AimAttackState : IState
    {
        int currentFrame;
        int updatePerFrame;
        float attackDistance;
        Transform parent;
        Health target;

        bool CanAttack => Vector2.Distance(target.transform.position, parent.position) <= attackDistance;
        public bool CanSelfEnter => false;
        
        public event UnityAction<Health> AttackTarget;

        public AimAttackState(Transform parent, int updatePerFrame)
        {
            this.parent = parent;
            this.updatePerFrame = updatePerFrame;
        }

        public void Init(float attackDistance)
        {
            this.attackDistance = attackDistance;
        }

        public void SetTarget(Health target)
        {
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