using Game.Grades.AllyCharacters;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Characters.Parameters
{
    [CreateAssetMenu(menuName = "Characters/Parameters/AllyMeleeUnitParameters")]
    public class AllyMeleeParameters: ScriptableObject
    {
        [SerializeField]
        AllyMeleeCountGrades countGrades;
        
        [SerializeField]
        int maxInLineCount;
        
        [SerializeField]
        int hp = 20;

        [SerializeField]
        AllyMeleeDamageGrades damageGrades;

        [SerializeField]
        Vector2 moveVector = new (2, 0);
        
        [SerializeField]
        float attackDistance = 0.5f;
        
        public int MeleeCount => countGrades.CharactersCount;
        public int MaxInLineCount => maxInLineCount;
        public Vector2 MoveVector => moveVector;
        public float MoveSpeed => Mathf.Abs(moveVector.x);
        public float Damage => damageGrades.Damage;
        public float AttackDistance => attackDistance;
        public int Hp => hp;
    }
}