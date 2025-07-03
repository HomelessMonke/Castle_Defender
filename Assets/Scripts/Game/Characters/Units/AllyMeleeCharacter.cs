using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.Units
{
    [RequireComponent(typeof(Health))]
    public class AllyMeleeCharacter: Character, IDamageable
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
        
        [SerializeField]
        int updatePathPerFrame = 10;
        
#if UNITY_EDITOR
        [SerializeField]
        Color pathLineColor = Color.blue;
#endif
        
        [SerializeField]
        DamageFlash damageFlash;

        string id;
        
        MeleeAttack meleeAttack;
        
        AnimatedIdleState idleState;
        NoneTargetMoveState noneTargetMoveState;
        TargetMoveState targetMoveState;
        AnimatedAttackState attackState;

        public string Id => id;
        public bool IsAlive => health.IsAlive;
        public Health HealthComponent => health;
        public Transform Transform => transform;
        
        public event UnityAction Died;
        
        void Awake()
        {
            meleeAttack = new MeleeAttack();
            attackState = new AnimatedAttackState(meleeAttack, characterAnimator);
            attackState.LoseTargetToAttack += OnLoseTargetToAttack;
            
            idleState = new AnimatedIdleState(characterAnimator.Animator);
            noneTargetMoveState = new NoneTargetMoveState(transform, characterAnimator.Animator);
            targetMoveState = new TargetMoveState(agent, characterAnimator.Animator, updatePathPerFrame);
            targetMoveState.ArrivedToTarget += OnArrivedToTarget;
            
            fieldOfView.TargetUpdated += OnTargetChanged;
        }

        public void Init(string id, AllyMeleeParameters parameters)
        {
            this.id = id;
            healthView.SetActive(false);
            health.Init(parameters.HealthPoints);
            health.DamageTaken += (_)=> OnGetDamage();
            health.Died += OnDeath;

            fieldOfView.Init();
            noneTargetMoveState.Init(parameters.MoveVector);
            targetMoveState.Init(parameters.MoveSpeed, parameters.AttackDistance);
            SetAttackParameter(parameters.Damage);
            SetState(idleState);
            
            spriteRenderer.sortingOrder = sortingLayerMedium - Mathf.RoundToInt(transform.position.y * sortingLayerMultiplier);
        }

        public void SetImmortal(bool isImmortal)
        {
            health.SetImmortal(isImmortal);
        }

        public void SetHealthPoints(float healthPoints)
        {
            health.SetHealth(healthPoints);
        }
        
        public void SetAttackParameter(float damage)
        {
            attackState.Init(damage);
        }
        
        void OnGetDamage()
        {
            if (!health.IsAlive || health.IsImmortal)
                return;
            
            healthView.Draw(health);
            damageFlash.Flash();

            if (currentState.Equals(idleState))
            {
                SetMoveState();
            }
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
        
        bool TrySetAttackState(IDamageable target)
        {
            if (target == null)
                return false;
                
            if (target.IsAlive)
            {
                attackState.SetTarget(target);
                SetState(attackState);
                return true;
            }
            return false;
        }
        
        void OnLoseTargetToAttack()
        {
            fieldOfView.UpdateTarget();
        }
        
        void OnDeath()
        {
            Died?.Invoke();
            Reset();
        }
        
        public void Reset()
        {
            if (currentState != null)
            {
                currentState.Exit();
                currentState = null;
            }
            Died = null;
        }
        
        public override void SetIdleState()
        {
            SetState(idleState);
        }
    }
}