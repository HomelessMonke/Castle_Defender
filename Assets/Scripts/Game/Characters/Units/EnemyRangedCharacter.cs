using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.Projectiles;
using Game.Characters.Spawners;
using Game.Characters.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.Units
{
    [RequireComponent(typeof(Health))]
    public class EnemyRangedCharacter: Character
    {
        [SerializeField]
        CharacterAnimator characterAnimator;

        [SerializeField]
        CharacterFieldOfView fov;

        [SerializeField]
        Transform projSpawnPos;

        [SerializeField]
        ProjectileAnimationData animationData;
        
        [SerializeField]
        Health health;
        
        [SerializeField]
        CharacterHealthView healthView;
        
        [SerializeField]
        NavMeshAgent agent;

        [SerializeField]
        int updatePathPerFrame = 10;

#if UNITY_EDITOR
        [SerializeField]
        Color pathLineColor = Color.red;
#endif
        [SerializeField]
        DamageFlash damageFlash;
        
        RangedAttack rangedAttack;
        
        AnimatedAttackState attackState;
        NoneTargetMoveState noneTargetMoveState;
        TargetMoveState targetMoveState;
        
        public event UnityAction Died;
        
        void Awake()
        {
            noneTargetMoveState = new NoneTargetMoveState(transform, characterAnimator.Animator);
            targetMoveState = new TargetMoveState(agent, characterAnimator.Animator, updatePathPerFrame);
            targetMoveState.ArrivedToTarget += OnArrivedToTarget;
            
            rangedAttack = new RangedAttack(projSpawnPos, animationData);
            attackState = new AnimatedAttackState(rangedAttack, characterAnimator);
            attackState.LoseTargetToAttack += OnLoseTargetToAttack;
            fov.TargetChanged += UpdateTarget;
        }

        public void Init(EnemyRangedParameters parameters, ProjectileSpawner projSpawner)
        {
            healthView.SetActive(false);
            health.Init(parameters.Hp);
            health.DamageTaken += (_)=> OnGetDamage();
            health.Died += OnDeath;

            fov.Init(transform);
            rangedAttack.Init(parameters.ProjectileSpeed, projSpawner);
            noneTargetMoveState.Init(parameters.MoveDirection);
            targetMoveState.Init(parameters.Speed, parameters.AttackDistance);
            attackState.Init(parameters.Damage);
            SetMoveState();
        }
        
        void OnGetDamage()
        {
            healthView.Draw(health);
            damageFlash.Flash();
        }

        void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
#if UNITY_EDITOR
            Debug.DrawLine(transform.position, agent.destination, pathLineColor);
#endif
        }

        void UpdateTarget(Transform target)
        {
            if (target && target.gameObject.activeSelf)
            {
                var targetDistance = Vector2.Distance(transform.position, target.position);
                if (targetDistance > agent.stoppingDistance)
                {
                    SetMoveState(target);
                    return;
                }

                if (TrySetAttackState(target))
                {
                    return;
                }
            }
            
            SetMoveState();
        }
        
        void SetMoveState(Transform target = null)
        {
            IState state = target? targetMoveState : noneTargetMoveState;
            SetState(state);
        }
        
        bool TrySetAttackState(Transform target)
        {
            if (!target)
                return false;
                
            var targetHP = target.GetComponent<Health>();
            if (targetHP && targetHP.IsAlive)
            {
                attackState.SetTarget(targetHP);
                SetState(attackState);
                return true;
            }
            return false;
        }
        
        void OnArrivedToTarget(Transform target)
        {
            if(!TrySetAttackState(target))
            {
                SetMoveState();
            }
        }
        
        void OnLoseTargetToAttack()
        {
            UpdateTarget(fov.CurrentTarget);
        }

        void OnDeath()
        {
            if(currentState != null)
                currentState.Exit();
            currentState = null;
            Died?.Invoke();
            Died = null;
        }
    }
}