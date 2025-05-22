using Game.Grades.AllyArchersGrades;
using UnityEngine;

namespace Game.Characters.Parameters
{
    [CreateAssetMenu(menuName = "Characters/Parameters/AllyArchersParameters")]
    public class AllyArchersParameters : ScriptableObject
    {
        [SerializeField]
        AllyArchersCountGrades countGrades;
        
        [Header("Макс. кол-во лучников в ряду")]
        [SerializeField]
        int maxInLine;

        [SerializeField]
        AllyArchersDamageGrades damageGrades;

        [SerializeField]
        float projectileSpeed = 5.5f;

        [SerializeField]
        float attackCD = 4;

        [SerializeField]
        float attackDistance = 10;
        
        public int ArchersCount => countGrades.ArchersCount;
        public float Damage => damageGrades.Damage;
        public int MaxInLine => maxInLine;
        public float ProjectileSpeed => projectileSpeed;
        public float AttackCD => attackCD;
        public float AttackDistance => attackDistance;
    }
}