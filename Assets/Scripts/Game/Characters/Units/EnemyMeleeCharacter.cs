﻿using Game.Characters.Attacks;
using Game.Characters.Parameters;
using Game.Characters.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Characters.Units
{
    [RequireComponent(typeof(Health))]
    public class EnemyMeleeCharacter: Character
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
        
#if UNITY_EDITOR
        [SerializeField]
        Color pathLineColor = Color.red;
#endif

        [SerializeField]
        int updatePathPerFrame = 10;
        
        [SerializeField]
        DamageFlash damageFlash;

        MeleeAttack meleeAttack;

        AnimatedAttackState attackState;
        NoneTargetMoveState noneTargetMoveState;
        TargetMoveState targetMoveState;
        
        public event UnityAction Died;
        
        void Awake()
        {
            meleeAttack = new MeleeAttack();
            attackState = new AnimatedAttackState(meleeAttack, characterAnimator);
            attackState.LoseTargetToAttack += OnLoseTargetToAttack;
            
            noneTargetMoveState = new NoneTargetMoveState(transform, characterAnimator.Animator);
            targetMoveState = new TargetMoveState(agent, characterAnimator.Animator, updatePathPerFrame);
            targetMoveState.ArrivedToTarget += OnArrivedToTarget;
            
            fieldOfView.TargetChanged += OnTargetChanged;
        }

        public void Init(EnemyMeleeParameters parameters)
        {
            healthView.SetActive(false);
            health.Init(parameters.Hp);
            health.DamageTaken += (_)=> OnGetDamage();
            health.Died += OnDeath;

            fieldOfView.Init(transform);
            noneTargetMoveState.Init(parameters.MoveDirection);
            targetMoveState.Init(parameters.Speed, parameters.AttackDistance);
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
#if UNITY_EDITOR
            Debug.DrawLine(transform.position, agent.destination, pathLineColor);
#endif
        }

        void SetMoveState(Transform target = null)
        {
            if (target)
            {
                targetMoveState.SwitchTarget(target);
                SetState(targetMoveState);
                return;
            } 
            
            SetState(noneTargetMoveState);
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
    }

}