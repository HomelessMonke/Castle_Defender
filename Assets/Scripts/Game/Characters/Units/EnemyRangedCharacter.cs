using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.Projectiles;
using Game.Characters.Spawners;
using Game.Characters.States;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;
using Utilities.Attributes;

namespace Game.Characters.Units
{
    [RequireComponent(typeof(Health))]
    public class EnemyRangedCharacter: Character
    {
        [SerializeField]
        CharacterType characterType;
            
        [SerializeField]
        TargetsDetectionArea fov;

        [SerializeField]
        Transform projSpawnPos;

        [SerializeField]
        ProjectileAnimationData animationData;
        
        [SerializeField]
        Health health;
        
        [SerializeField]
        HealthView healthView;
        
        [SerializeField]
        NavMeshAgent agent;
        
        [SerializeField]
        Color pathLineColor = Color.red;

        [SerializeField]
        DamageFlash damageFlash;
        
        RangedAttack rangedAttack;
        AttackState attackState;
        MoveState moveState;
        
        CustomTimer timer;

        float attackDistance;
        float fovDistance;
        
        public event UnityAction Died;
        
        void Awake()
        {
            moveState = new MoveState(agent);
            moveState.ArrivedToTarget += OnArrivedToTarget;
            
            rangedAttack = new RangedAttack(projSpawnPos, animationData);
            attackState = new AttackState(rangedAttack);
            attackState.LoseTargetToAttack += UpdateTarget;
            
            timer = new CustomTimer() {Repeatable = true};
            timer.TimerEnd += UpdateTarget;
        }

        public void Init(EnemyRangedParameters parameters, ProjectileSpawner projSpawner)
        {
            attackDistance = parameters.AttackDistance;
            fovDistance = parameters.FovDistance;
            timer.SetDuration(parameters.UpdateFovCD);
            timer.Start();
            
            healthView.SetActive(false);
            health.Init(parameters.Hp);
            health.DamageTaken += (_)=> OnGetDamage();
            health.Died += OnDeath;

            rangedAttack.Init(parameters.ProjectileSpeed, projSpawner);
            moveState.Init(parameters.MoveDirection, parameters.AttackDistance);
            attackState.Init(parameters.Damage, parameters.AttackCD);
            SetMoveState();
        }
        
        void OnGetDamage()
        {
            healthView.Draw(health);
            damageFlash.Flash();
        }

        void Update()
        {
            timer.Tick(Time.deltaTime);
            if (currentState != null)
            {
                currentState.Update();
            }
            Debug.DrawLine(transform.position, agent.destination, pathLineColor);
        }

        void SetMoveState(Transform target = null)
        {
            moveState.SetTargetObj(target);
            SetState(moveState);
        }

        void UpdateTarget()
        {
            var target = fov.GetTargetByDistance(transform.position);
            
            if (target && target.IsAlive)
            {
                var targetDistance = Vector2.Distance(transform.position, target.transform.position);
                if (targetDistance <= attackDistance)
                {
                    attackState.SetTarget(target);
                    SetState(attackState);
                    return;
                }

                if (targetDistance <= fovDistance)
                {
                    moveState.SetTargetObj(target.transform);
                    SetState(moveState);
                    return;
                }
            }
            
            SetMoveState();
        }

        void OnArrivedToTarget(Transform target)
        {
            if (target && target.TryGetComponent(out Health health))
            {
                if (health.IsAlive)
                {
                    attackState.SetTarget(health);
                    SetState(attackState);
                    return;
                }
            }
            
            UpdateTarget();
        }

        void OnDeath()
        {
            currentState.Exit();
            currentState = null;
            Died?.Invoke();
            Died = null;
        }

        [Button]
        void SetOneHP()
        {
            health.GetDamage(health.MaxHealth-1);
        }
    }
}