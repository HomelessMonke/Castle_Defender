using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.States;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities.Attributes;

namespace Game.Characters.Units
{
    [RequireComponent(typeof(HealthComponent))]
    public class SwordsManCharacter: Character
    {
        [SerializeField]
        CharacterFieldOfView fieldOfView;
        
        [SerializeField]
        HealthComponent health;
        
        [SerializeField]
        HealthView healthView;
        
        [SerializeField]
        NavMeshAgent agent;
        
        [SerializeField]
        Color pathLineColor = Color.red;

        [SerializeField]
        DamageFlash damageFlash;

        MeleeAttack meleeAttack;

        AttackState attackState;
        MoveState moveState;
        
        Vector2 mainTargetPos;
        
        public event UnityAction Died;

        void Awake()
        {
            moveState = new MoveState(agent);
            meleeAttack = new MeleeAttack();
            attackState = new AttackState(meleeAttack);
            moveState.ArrivedToTarget += OnArrivedToTarget;
            attackState.LoseTargetToAttack += OnLoseTargetToAttack;
            fieldOfView.TargetChanged += OnTargetChanged;
        }

        public void Init(Vector2 mainTargetPos, MeleeUnitParameters parameters)
        {
            this.mainTargetPos = mainTargetPos;
            
            healthView.SetActive(false);
            health.Init(parameters.Hp);
            health.DamageTaken += OnGetDamage;
            health.Died += OnDeath;

            fieldOfView.Init(transform);
            moveState.Init(parameters.MoveSpeed, parameters.AttackDistance);
            attackState.Init(parameters.AttackPoints, parameters.AttackCD);
            SetMoveState();
        }
        
        void OnGetDamage()
        {
            healthView.Draw(health);
            damageFlash.Flash();
        }

        void Update()
        {
            fieldOfView.UpdateViewTarget();
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
                
            var targetHP = target.GetComponent<HealthComponent>();
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
            if (target)
                moveState.SetTargetObj(target);
            else
                moveState.SetTargetPos(mainTargetPos);
            
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