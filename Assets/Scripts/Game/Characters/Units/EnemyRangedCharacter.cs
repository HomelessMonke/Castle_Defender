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
    public class EnemyRangedCharacter: Character, IDamageable
    {
        [SerializeField]
        SpriteRenderer spriteRenderer;
        
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
        
        AnimatedIdleState idleState;
        AnimatedAttackState attackState;
        NoneTargetMoveState noneTargetMoveState;
        TargetMoveState targetMoveState;
        
        string id;
        
        public string Id => id;
        public bool IsAlive => health.IsAlive;
        public Transform Transform => transform;
        public Health HealthComponent => health;
        
        public event UnityAction Died;
        
        void Awake()
        {
            noneTargetMoveState = new NoneTargetMoveState(transform, characterAnimator.Animator);
            targetMoveState = new TargetMoveState(agent, characterAnimator.Animator, updatePathPerFrame);
            targetMoveState.ArrivedToTarget += OnArrivedToTarget;
            
            idleState = new AnimatedIdleState(characterAnimator.Animator);
            rangedAttack = new RangedAttack(projSpawnPos, animationData);
            attackState = new AnimatedAttackState(rangedAttack, characterAnimator);
            attackState.LoseTargetToAttack += OnLoseTargetToAttack;
            fov.TargetUpdated += UpdateTarget;
        }

        public void Init(string id, EnemyRangedParameters parameters, ProjectileSpawner projSpawner)
        {
            this.id = id;
            healthView.SetActive(false);
            health.Init(parameters.Hp);
            health.DamageTaken += (_)=> OnGetDamage();
            health.Died += OnDeath;

            fov.Init();
            rangedAttack.Init(parameters.ProjectileSpeed, projSpawner);
            noneTargetMoveState.Init(parameters.MoveDirection);
            targetMoveState.Init(parameters.Speed, parameters.AttackDistance);
            attackState.Init(parameters.Damage);
            SetMoveState();
            
            spriteRenderer.sortingOrder = sortingLayerMedium - Mathf.RoundToInt(transform.position.y * sortingLayerMultiplier);
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

        void UpdateTarget(IDamageable target)
        {
            if (target != null && target.IsAlive)
            {
                var targetDistance = Vector2.Distance(transform.position, target.Transform.position);
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
        
        void SetMoveState(IDamageable target = null)
        {
            IState state = target != null ? targetMoveState : noneTargetMoveState;
            SetState(state);
        }
        
        bool TrySetAttackState(IDamageable target)
        {
            if (target == null)
                return false;
            
            if (target.HealthComponent && target.IsAlive)
            {
                attackState.SetTarget(target);
                SetState(attackState);
                return true;
            }
            return false;
        }
        
        void OnArrivedToTarget(IDamageable target)
        {
            if(!TrySetAttackState(target))
            {
                SetMoveState();
            }
        }
        
        void OnLoseTargetToAttack()
        {
            fov.UpdateTarget();
        }

        void OnDeath()
        {
            if(currentState != null)
                currentState.Exit();
            currentState = null;
            Died?.Invoke();
            Died = null;
        }
        
        public void Reset()
        {
            currentState = null;
            Died = null;
        }
        
        public override void SetIdleState()
        {
            SetState(idleState);
        }
    }
}