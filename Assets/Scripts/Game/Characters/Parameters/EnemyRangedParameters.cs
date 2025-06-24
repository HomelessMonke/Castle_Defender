using UnityEngine;
namespace Game.Characters.Parameters
{
    [CreateAssetMenu(menuName = "Characters/Parameters/EnemyRangedParameters")]
    public class EnemyRangedParameters: ScriptableObject
    {
        [SerializeField]
        int hp = 5;
        
        [SerializeField]
        Vector2 moveDirection = new Vector2(-1, 0);
        
        [SerializeField]
        int damage = 3;
        
        [SerializeField]
        float projectileSpeed = 5.5f;

        [SerializeField]
        float attackDistance = 6;
        
        [Space(10)]
        [Header("Награда за уничтожение врага")]
        [SerializeField]
        int minCoinsReward = 5;
        
        [SerializeField]
        int maxCoinsReward = 10;
        
        public int Hp => hp;
        public int Damage => damage;
        public Vector2 MoveDirection => moveDirection;
        public float Speed => Mathf.Abs(moveDirection.x);
        public float ProjectileSpeed => projectileSpeed;
        public float AttackDistance => attackDistance;
        public int CoinReward => Random.Range(minCoinsReward, maxCoinsReward);
    }
}