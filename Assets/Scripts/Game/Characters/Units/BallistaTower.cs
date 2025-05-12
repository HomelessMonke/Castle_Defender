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
            aimState = new AimAttackState(transform);
            attackState = new NotAnimatedAttackState(rangedAttack);
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

        public void Init(BallistaTowerParameters parameters, ProjectileSpawner projectileSpawner)
        {
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

            var target = fov.GetTargetByDistance(transform.position);
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