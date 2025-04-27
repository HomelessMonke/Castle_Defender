using System;
using Game.Characters.Attacks;
using Game.Characters.Projectiles;
using Game.Characters.Spawners;
using Game.Characters.States;
using UnityEngine;

namespace Game.Characters.Units
{
    /// <summary>
    /// Башня которая может стрелять любыми видами снарядов
    /// принимает параметры 
    /// </summary>
    public class ShootingTower : Character
    {
        [SerializeField]
        Transform projectileSpawnPoint;
        
        [SerializeField]
        MultiTargetFieldOfView fov;

        [SerializeField]
        ProjectileAnimationData animationData;
        
        [Header("Индекс для fov")]
        [SerializeField]
        int index;

        IdleState idleState;
        AttackState attackState;
        RangedAttack rangedAttack;

        void Awake()
        {
            rangedAttack = new RangedAttack(projectileSpawnPoint, animationData);
            idleState = new IdleState();
            attackState = new AttackState(rangedAttack);
            attackState.LoseTargetToAttack += OnUpdateTargets;
            fov.TargetsChanged += OnUpdateTargets;
        }

        void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }

        public void Init(int attackCD, int damage, float speed, ProjectileSpawner projectileSpawner)
        {
            rangedAttack.Init(speed, projectileSpawner);
            attackState.Init(damage, attackCD);
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
            attackState.SetTarget(target);
            SetState(attackState);
        }
    }
}