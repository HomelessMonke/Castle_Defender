using UnityEngine;

namespace Game.Waves
{
    [CreateAssetMenu(fileName = "WavesList", menuName = "Waves/WavesList")]
    public class WavesList : ScriptableObject
    {
        [SerializeField]
        Wave[] waves;
        
        [SerializeField]
        float timeBetweenWaves = 5f;
        
        public Wave[] Waves => waves;
        public float TimeBetweenWaves => timeBetweenWaves;
        
        //TODO: Добавить сейв int каунтер последней волны.
    }
}