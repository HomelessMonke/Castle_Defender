using System;
using Game.Characters.States;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters
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
        
        AttackState attackState;
        MoveState moveState;
        
        Transform mainTarget;
        
        public event UnityAction Died;

        void Awake()
        {
            moveState = new MoveState(agent);
            attackState = new AttackState(fieldOfView);
            moveState.ArrivedToTarget += SetAttackState;
            attackState.LoseTargetToAttack += ()=> SetMoveState();
            fieldOfView.TargetChanged += OnTargetChanged;
        }

        public void Init(Transform mainTarget, CharacterParameters parameters)
        {
            this.mainTarget = mainTarget;
            
            healthView.SetActive(false);
            health.Init(parameters.Hp);
            health.DamageTaken += OnGetDamage;
            health.Died += OnDeath;

            fieldOfView.Init(transform);
            moveState.Init(parameters.MoveSpeed, parameters.AttackDistance);
            attackState.Init(parameters.AttackPoints, parameters.AttackCD);
            SetMoveState(mainTarget);
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

        public void SetAttackState()
        {
            ChangeState(attackState);
        }

        void SetMoveState(Transform target = null)
        {
            var newTarget = target? target : mainTarget;
            var isStatic = newTarget.gameObject.isStatic;
            moveState.SetTarget(newTarget, isStatic);
            ChangeState(moveState);
        }

        void OnTargetChanged(Transform targetTransform)
        {
            if (targetTransform && targetTransform.gameObject.activeSelf)
            {
                var targetDistance = Vector2.Distance(transform.position, targetTransform.position);
                if (targetDistance > agent.stoppingDistance)
                {
                    SetMoveState(targetTransform);
                }
            }
            else
            {
                SetMoveState();
            }
        }
        
        void OnDeath()
        {
            currentState.Exit();
            currentState = null;
            Died?.Invoke();
            Died = null;
        }
    }
}