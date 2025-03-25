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
        
        public int AttackPoints => attackPoints;
        public float MoveSpeed => moveSpeed;
        public int Hp => hp;
    }
}