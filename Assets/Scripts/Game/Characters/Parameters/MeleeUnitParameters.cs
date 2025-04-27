using UnityEngine;
namespace Game.Characters.Parameters
{
    [CreateAssetMenu(menuName = "Characters/Parameters/MeleeUnitParameters")]
    public class MeleeUnitParameters: ScriptableObject
    {
        [SerializeField]
        int hp = 20;

        [SerializeField]
        int attackPoints = 4;
        
        [SerializeField]
        float moveSpeed = 2;

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