using System;
using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.Projectiles;
using Game.Characters.Spawners;
using Game.Characters.States;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

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

        void OnUpdateTargets()
        {
            (var target, bool inRange) = fov.GetRandomTargetInRange(transform.position, attackDistance);
            if (!target)
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

        void OnAimStateCanAttack(Health target)
        {
            attackState.SetTarget(target);
            SetState(attackState);
        }
    }
}