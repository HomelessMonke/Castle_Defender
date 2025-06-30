using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.Projectiles;
using Game.Characters.Spawners;
using Game.Characters.States;
using UnityEngine;

namespace Game.Characters.Units
{
    public class BallistaTower : Character
    {
        [SerializeField]
        Transform projectileSpawnPoint;
        
        [SerializeField]
        TargetsDetectionArea fov;

        [SerializeField]
        ProjectileAnimationData animationData;
        
        float attackDistance;

        NotAnimatedIdleState idleState;
        AimAttackState aimState;
        NotAnimatedAttackState attackState;
        
        RangedAttack rangedAttack;
        
        void Awake()
        {
            rangedAttack = new RangedAttack(projectileSpawnPoint, animationData);
            idleState = new NotAnimatedIdleState();
            aimState = new AimAttackState(transform, 10);
            attackState = new NotAnimatedAttackState(rangedAttack);
            attackState.LoseTargetToAttack += UpdateTarget;
            aimState.AttackTarget += OnAimStateCanAttack;
            aimState.LoseTargetToAim += UpdateTarget;
            fov.TargetsUpdated += UpdateTarget;
        }

        void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }

        public void Init(BallistaTowerParameters parameters, ProjectileSpawner projectileSpawner)
        {
            attackDistance = parameters.AttackDistance;
            aimState.Init(attackDistance);
            rangedAttack.Init(parameters.ProjectileSpeed, projectileSpawner);
            attackState.Init(parameters.Damage, parameters.AttackCD);
            SetState(idleState);
        }

        void UpdateTarget()
        {
            (var target, bool inRange) = fov.GetClosestTarget(transform.position, attackDistance);
            if (target == null)
            {
                SetState(idleState);
                return;
            }
            
            if (inRange)
            {
                attackState.SetTarget(target);
                SetState(attackState);
            }
            else
            {
                aimState.SetTarget(target);
                SetState(aimState);
            }
        }

        void OnAimStateCanAttack(IDamageable target)
        {
            attackState.SetTarget(target);
            SetState(attackState);
        }
    }
}