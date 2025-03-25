using UnityEngine;

namespace Game.Waves
{
    [CreateAssetMenu(fileName = "WavesList", menuName = "Waves/WavesList")]
    public class WavesList : ScriptableObject
    {
        [SerializeField]
        Wave[] waves;
        
        public Wave[] Waves => waves;
    }
}