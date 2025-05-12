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

        int index;
        float attackDistance;
        bool lookTargetByDistance;
        
        AnimatedIdleState idleState;
        AnimatedAttackState attackState;
        AimAttackState aimState;
        RangedAttack rangedAttack;
        
        void Awake()
        {
            rangedAttack = new RangedAttack(projectileSpawnPoint, animationData);
            idleState = new AnimatedIdleState(characterAnimator.Animator);
            aimState = new AimAttackState(transform);
            attackState = new AnimatedAttackState(rangedAttack, characterAnimator);
            attackState.LoseTargetToAttack += OnUpdateTargets;
            aimState.AttackTarget += OnAimStateCanAttack;
            fov.TargetsChanged += OnUpdateTargets;
        }

        void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }

        public void Init(int index, bool lookTargetByDistance, AllyArchersParameters parameters, ProjectileSpawner projectileSpawner)
        {
            this.index = index;
            this.lookTargetByDistance = lookTargetByDistance;
            attackDistance = parameters.AttackDistance;
            rangedAttack.Init(parameters.ProjectileSpeed, projectileSpawner);
            attackState.Init(parameters.Damage, parameters.AttackCD);
            SetState(idleState);
        }

        void OnUpdateTargets()
        {
            if (!fov.HaveTargets)
            {
                SetState(idleState);
                return;
            }

            var target = lookTargetByDistance? fov.GetTargetByDistance(transform.position): fov.GetTargetByIndex(index);
            var distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance > attackDistance)
            {
                aimState.SetTarget(target, attackDistance);
                SetState(aimState);
            }
            else
            {
                attackState.SetTarget(target);
                SetState(attackState);
            }
        }

        void OnAimStateCanAttack(Health target)
        {
            attackState.SetTarget(target);
            SetState(attackState);
        }
    }
}