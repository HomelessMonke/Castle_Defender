using UnityEngine;
namespace Game.Characters.Parameters
{

    [CreateAssetMenu(menuName = "Characters/Parameters/EnemyMeleeUnitParameters")]
    public class EnemyMeleeUnitParameters: MeleeUnitParameters
    {
        [Space(10)]
        [Header("Награда за уничтожение врага")]
        [SerializeField]
        int minCoinsReward = 20;
        
        [SerializeField]
        int maxCoinsReward = 40;
        
        public int CoinReward => Random.Range(minCoinsReward, maxCoinsReward);
    }
}