using Game.Characters.Units;
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
        
        IDamageable target;
        string targetId;

        bool CanAttack => Vector2.Distance(target.Transform.position, parent.position) <= attackDistance;
        public bool CanSelfEnter => false;
        
        public event UnityAction<IDamageable> AttackTarget;
        public event UnityAction LoseTargetToAim;

        public AimAttackState(Transform parent, int updatePerFrame)
        {
            this.parent = parent;
            this.updatePerFrame = updatePerFrame;
        }

        public void Init(float attackDistance)
        {
            this.attackDistance = attackDistance;
        }

        public void SetTarget(IDamageable target)
        {
            this.target = target;
            targetId = target.Id;
        }

        public void Enter()
        {
           
        }
        
        public void Update()
        {
            if (!target.IsAlive || !targetId.Equals(target.Id))
            {
                LoseTargetToAim?.Invoke();
                return;
            }
            
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