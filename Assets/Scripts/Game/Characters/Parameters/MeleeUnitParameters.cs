using UnityEngine;

namespace Game.Characters.Parameters
{
    public class MeleeUnitParameters: ScriptableObject
    {
        [SerializeField]
        int hp = 20;

        [SerializeField]
        int attackPoints = 4;
        
        [SerializeField]
        Vector2 moveDirection = new Vector2(2, 0);

        [SerializeField]
        float attackDistance = 0.5f;
        
        [SerializeField]
        float attackCD = 2;
        
        public int AttackPoints => attackPoints;
        public Vector2 MoveDirection => moveDirection;
        public float AttackDistance => attackDistance;
        public float AttackCD => attackCD;
        public int Hp => hp;
    }
}