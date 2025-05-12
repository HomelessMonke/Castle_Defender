using System;
using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator: MonoBehaviour
    {
        [SerializeField]
        Animator animator;
        
        public Animator Animator => animator;
        
        public event Action AttackEvent;
        
        void HandleAttackEvent()
        {
            AttackEvent?.Invoke();
        }
    }
}