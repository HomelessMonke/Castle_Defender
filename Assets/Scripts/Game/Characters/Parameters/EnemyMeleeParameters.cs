using UnityEngine;
namespace Game.Characters.Parameters
{

    [CreateAssetMenu(menuName = "Characters/Parameters/EnemyMeleeUnitParameters")]
    public class EnemyMeleeParameters: ScriptableObject
    {
        [Space(10)]
        [Header("Награда за уничтожение врага")]
        [SerializeField]
        int minCoinsReward = 20;
        
        [SerializeField]
        int maxCoinsReward = 40;
        
        [SerializeField]
        int hp = 20;

        [SerializeField]
        int attackPoints = 4;
        
        [SerializeField]
        Vector2 moveDirection = new Vector2(2, 0);

        [SerializeField]
        float attackDistance = 0.5f;
        
        public int CoinReward => Random.Range(minCoinsReward, maxCoinsReward);
        public Vector2 MoveDirection => moveDirection;
        public float Speed => Mathf.Abs(moveDirection.x);
        public int AttackPoints => attackPoints;
        public float AttackDistance => attackDistance;
        public int Hp => hp;
    }
}