﻿using UnityEngine;
namespace Game.Characters.Parameters
{
    [CreateAssetMenu(menuName = "Characters/Parameters/AllyArchersParameters")]
    public class AllyArchersParameters : ScriptableObject
    {
        [SerializeField]
        int damage = 3;
        
        [SerializeField]
        float projectileSpeed = 5.5f;
        
        [SerializeField]
        float attackCD = 4;

        [SerializeField]
        float attackDistance = 10;
        
        public int Damage => damage;
        public float ProjectileSpeed => projectileSpeed;
        public float AttackCD => attackCD;
        public float AttackDistance => attackDistance;
    }
}