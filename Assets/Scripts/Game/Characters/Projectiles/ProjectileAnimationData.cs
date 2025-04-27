using System;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Game.Characters.Projectiles
{
    [Serializable]
    public class ProjectileAnimationData
    {
        [SerializeField]
        float yMinOffset, yMaxOffset;
        
        [Header("Кривая множителя сдвига траектории по Y")]
        [SerializeField]
        AnimationCurve yCurve;
        
        [Header("Кривая скорости движения снаряда")]
        [SerializeField]
        AnimationCurve xCurve;
        
        public float YOffset => Random.Range(yMinOffset, yMaxOffset);
        public AnimationCurve YCurve => yCurve;
        public AnimationCurve XCurve => xCurve;
    }
}