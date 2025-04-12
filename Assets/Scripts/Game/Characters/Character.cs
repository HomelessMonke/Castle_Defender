using System;
using System.Text;
using Game.Characters.States;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities.Attributes;

namespace Game.Characters
{
    public class Character: MonoBehaviour
    {
        [SerializeField]
        CharacterFieldOfView fieldOfView;
        
        [SerializeField]
        NavMeshAgent agent;
        
        [SerializeField]
        HealthView healthView;
        
        [SerializeField]
        protected HealthComponent health;
        
        [SerializeField]
        DamageFlash damageFlash;
        
        Transform mainTarget;
        CharacterParameters parameters;
        
        AttackState attackState;
        MoveState moveState;
        IState currentState;
        
        public StringBuilder stateLog = new StringBuilder();
        public bool IsAlive => health.IsAlive;
        public Transform FovTarget => fieldOfView.CurrentTarget;
        public CharacterParameters Parameters => parameters;
        public NavMeshAgent NavAgent => agent;

        public event UnityAction Died;

        public void Init(Transform mainTarget, CharacterParameters parameters)
        {
            stateLog.Append("Character Init");
            stateLog.Append(Environment.NewLine);
            this.mainTarget = mainTarget;
            this.parameters = parameters;

            Died = null;
            
            health.Init(parameters.Hp);
            health.DamageTaken += OnGetDamage;
            health.Died += OnDeath;
            healthView.SetActive(false);

            fieldOfView.Init();
            fieldOfView.TargetChanged += OnTargetChanged;
            moveState = new MoveState(this);
            attackState = new AttackState(this);
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
        }

        public void SetAttackState()
        {
            stateLog.Append($"<color=purple>{nameof(SetAttackState)}</color>");
            stateLog.Append(Environment.NewLine);
            ChangeState(attackState);
        }

        public void SetMoveState(Transform target = null)
        {
            stateLog.Append($"<color=purple>{nameof(SetMoveState)}</color>");
            stateLog.Append(Environment.NewLine);
            var newTarget = target? target : mainTarget;
            var isStatic = newTarget.gameObject.isStatic;
            moveState.SetTarget(newTarget, isStatic);
            ChangeState(moveState);
        }

        void OnTargetChanged(Transform targetTransform)
        {
            stateLog.Append($"{nameof(OnTargetChanged)} ");
            if (targetTransform && targetTransform.gameObject.activeSelf)
            {
                stateLog.Append($"{targetTransform.name} isActive: {targetTransform.gameObject.activeSelf}");
                stateLog.Append(Environment.NewLine);

                var targetDistance = Vector2.Distance(transform.position, targetTransform.position);
                stateLog.Append($"targetDistance:{targetDistance} > stoppingDistance:{agent.stoppingDistance}");
                stateLog.Append(Environment.NewLine);
                if (targetDistance > agent.stoppingDistance)
                {
                    SetMoveState(targetTransform);
                }
            }
            else
            {
                stateLog.Append($"targetTransform is NULL");
                stateLog.Append(Environment.NewLine);
                SetMoveState();
            }
        }

        void ChangeState(IState newState)
        {
            currentState?.Exit();
            newState.Enter();
            currentState = newState;
        }

        void OnDeath()
        {
            stateLog.Append("OnDeath");
            stateLog.Append(Environment.NewLine);
            currentState.Exit();
            currentState = null;
            Died?.Invoke();
        }

        [Button]
        void ShowLog()
        {
            Debug.Log(stateLog);
        }
    }
}