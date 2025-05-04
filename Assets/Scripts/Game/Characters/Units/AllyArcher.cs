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
        Transform projectileSpawnPoint;
        
        [SerializeField]
        MultiTargetFieldOfView fov;

        [SerializeField]
        ProjectileAnimationData animationData;

        int index;
        float attackDistance;
        bool lookTargetByDistance;
        
        IdleState idleState;
        AttackState attackState;
        AimAttackState aimState;
        RangedAttack rangedAttack;

        void Awake()
        {
            rangedAttack = new RangedAttack(projectileSpawnPoint, animationData);
            idleState = new IdleState();
            aimState = new AimAttackState(transform);
            attackState = new AttackState(rangedAttack);
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

        void OnAimStateCanAttack(HealthComponent target)
        {
            attackState.SetTarget(target);
            SetState(attackState);
        }
    }
}