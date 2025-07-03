using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.Projectiles;
using Game.Characters.Spawners;
using Game.Characters.States;
using UnityEngine;

namespace Game.Characters.Units
{
    public class AllyArcher : Character
    {
        [SerializeField]
        Animator animator;
        
        [SerializeField]
        CharacterAnimator characterAnimator;
        
        [SerializeField]
        Transform projectileSpawnPoint;
        
        [SerializeField]
        TargetsDetectionArea fov;

        [SerializeField]
        ProjectileAnimationData animationData;

        float attackDistance;
        
        AnimatedIdleState idleState;
        AnimatedAttackState attackState;
        AimAttackState aimState;
        RangedAttack rangedAttack;
        
        void Awake()
        {
            rangedAttack = new RangedAttack(projectileSpawnPoint, animationData);
            idleState = new AnimatedIdleState(characterAnimator.Animator);
            aimState = new AimAttackState(transform, 10);
            attackState = new AnimatedAttackState(rangedAttack, characterAnimator);
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

        public void Init(AllyArchersParameters parameters, ProjectileSpawner projectileSpawner)
        {
            attackDistance = parameters.AttackDistance;
            aimState.Init(attackDistance);
            rangedAttack.Init(parameters.ProjectileSpeed, projectileSpawner);
            SetAttackParameter(parameters.Damage);
            SetState(idleState);
        }

        public void SetAttackParameter(float damage)
        {
            attackState.Init(damage);
        }

        void UpdateTarget()
        {
            (var target, bool inRange) = fov.GetRandomTargetInRange(transform.position, attackDistance);
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
                animator.SetTrigger("Idle");
                aimState.SetTarget(target);
                SetState(aimState);
            }
        }

        void OnAimStateCanAttack(IDamageable target)
        {
            attackState.SetTarget(target);
            SetState(attackState);
        }

        public override void SetIdleState()
        {
            SetState(idleState);
        }
    }
}