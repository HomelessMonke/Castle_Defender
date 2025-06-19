using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities.Attributes;


namespace Game.Characters.Units
{
    [RequireComponent(typeof(Health))]
    public class MeleeCharacter: Character
    {
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
        Color pathLineColor = Color.red;

        [SerializeField]
        DamageFlash damageFlash;

        MeleeAttack meleeAttack;

        AnimatedAttackState attackState;
        MoveState moveState;
        
        public event UnityAction Died;
        
        void Awake()
        {
            moveState = new MoveState(agent, characterAnimator.Animator);
            meleeAttack = new MeleeAttack();
            attackState = new AnimatedAttackState(meleeAttack, characterAnimator);
            moveState.ArrivedToTarget += OnArrivedToTarget;
            attackState.LoseTargetToAttack += OnLoseTargetToAttack;
            fieldOfView.TargetChanged += OnTargetChanged;
        }

        public void Init(MeleeUnitParameters parameters)
        {
            healthView.SetActive(false);
            health.Init(parameters.Hp);
            health.DamageTaken += (_)=> OnGetDamage();
            health.Died += OnDeath;

            fieldOfView.Init(transform);
            moveState.Init(parameters.MoveDirection, parameters.AttackDistance);
            attackState.Init(parameters.AttackPoints);
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
            Debug.DrawLine(transform.position, agent.destination, pathLineColor);
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

        void SetMoveState(Transform target = null)
        {
            moveState.SetTarget(target);
            SetState(moveState);
        }

        void OnTargetChanged(Transform target)
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

        void OnArrivedToTarget(Transform target)
        {
            if(!TrySetAttackState(target))
            {
                SetMoveState();
            }
        }
        
        void OnLoseTargetToAttack()
        {
            OnTargetChanged(fieldOfView.CurrentTarget);
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

        [Button]
        void SetOneHP()
        {
            health.GetDamage(health.MaxHp-1);
        }
    }
}