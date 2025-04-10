using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(fileName = "CharacterInfo", menuName = "CharacterInfo")]
    public class CharacterParameters: ScriptableObject
    {
        [SerializeField]
        int hp;

        [SerializeField]
        int attackPoints;
        
        [SerializeField]
        float moveSpeed;

        [SerializeField]
        float attackDistance = 0.5f;
        
        [SerializeField]
        float attackCD = 2;
        
        public int AttackPoints => attackPoints;
        public float MoveSpeed => moveSpeed;
        public float AttackDistance => attackDistance;
        public float AttackCD => attackCD;
        public int Hp => hp;
    }
}