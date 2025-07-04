using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.Units
{
    [RequireComponent(typeof(Health))]
    public class EnemyMeleeCharacter: Character, IDamageable
    {
        [SerializeField]
        SpriteRenderer spriteRenderer;
        
        [SerializeField]
        CharacterFieldOfView fieldOfView;
        
        [SerializeField]
        CharacterAnimator characterAnimator;
        
        [SerializeField]
        Health health;
        
        [SerializeField]
        CharacterHealthView healthView;
        
        [SerializeField]
        NavMeshAgent agent;
        
#if UNITY_EDITOR
        [SerializeField]
        Color pathLineColor = Color.red;
#endif

        [SerializeField]
        int updatePathPerFrame = 10;
        
        [SerializeField]
        DamageFlash damageFlash;

        MeleeAttack meleeAttack;

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
            meleeAttack = new MeleeAttack();
            attackState = new AnimatedAttackState(meleeAttack, characterAnimator);
            attackState.LoseTargetToAttack += OnLoseTarget;
            
            idleState = new AnimatedIdleState(characterAnimator.Animator);
            noneTargetMoveState = new NoneTargetMoveState(transform, characterAnimator.Animator);
            targetMoveState = new TargetMoveState(agent, characterAnimator.Animator, updatePathPerFrame);
            targetMoveState.ArrivedToTarget += OnArrivedToTarget;
            targetMoveState.LoseTarget += OnLoseTarget;
            
            fieldOfView.TargetUpdated += OnTargetChanged;
        }

        public void Init(string id, EnemyMeleeParameters parameters, bool isSyncMove)
        {
            this.id = id;
            healthView.SetActive(false);
            health.Init(parameters.Hp);
            health.DamageTaken += (_)=> OnGetDamage();
            health.Died += OnDeath;

            fieldOfView.Init();
            noneTargetMoveState.Init(parameters.MoveDirection, isSyncMove);
            targetMoveState.Init(parameters.Speed, parameters.AttackDistance);
            attackState.Init(parameters.AttackPoints);
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

        void SetMoveState(IDamageable target = null)
        {
            if (target != null)
            {
                targetMoveState.SwitchTarget(target);
                SetState(targetMoveState);
                return;
            } 
            
            SetState(noneTargetMoveState);
        }

        void OnTargetChanged(IDamageable target)
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

        void OnArrivedToTarget(IDamageable target)
        {
            if(!TrySetAttackState(target))
            {
                SetMoveState();
            }
        }
        
        bool TrySetAttackState(IDamageable target)
        {
            if (target == null)
                return false;

            var targetHP = target.HealthComponent;
            if (targetHP && targetHP.IsAlive)
            {
                attackState.SetTarget(target);
                SetState(attackState);
                return true;
            }
            return false;
        }
        
        void OnLoseTarget()
        {
            fieldOfView.UpdateTarget();
        }
        
        void OnDeath()
        {
            if (currentState != null)
            {
                currentState.Exit();
                currentState = null;
            }
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