using Game.Grades.AllyCharacters;
using UnityEngine;

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
        int attackPoints = 4;

        [SerializeField]
        Vector2 moveDirection = new (2, 0);
        
        [SerializeField]
        float attackDistance = 0.5f;
        
        public int MeleeCount => countGrades.CharactersCount;
        public int MaxInLineCount => maxInLineCount;
        public Vector2 MoveDirection => moveDirection;
        public float Speed => Mathf.Abs(moveDirection.x);
        public int AttackPoints => attackPoints;
        public float AttackDistance => attackDistance;
        public int Hp => hp;
    }
}