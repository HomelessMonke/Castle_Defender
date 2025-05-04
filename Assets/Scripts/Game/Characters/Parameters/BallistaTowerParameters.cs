using UnityEngine;
namespace Game.Characters.Parameters
{
    [CreateAssetMenu(menuName = "Characters/Parameters/BallistaTowerParameters")]
    public class BallistaTowerParameters : ScriptableObject
    {
        [SerializeField]
        int damage = 10;
        
        [SerializeField]
        float projectileSpeed = 8;
        
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