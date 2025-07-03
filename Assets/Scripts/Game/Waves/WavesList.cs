using System;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Waves
{
    [CreateAssetMenu(fileName = "WavesList", menuName = "Waves/WavesList")]
    public class WavesList : ScriptableObject
    {
        [SerializeField]
        Wave[] waves;
        
        const string waveIndexSave = "WaveIndex";
        
        [NonSerialized]
        int nextWaveIndex = -1;

        public int WaveNumber => NextWaveIndex + 1;
        
        public Wave NextWave => waves[NextWaveIndex];
        
        int NextWaveIndex
        {
            get
            {
                if (nextWaveIndex == -1)
                    InitWaveIndex();
                
                return nextWaveIndex;
            }
        }
        
        [Button]
        public void IncreaseNextWaveIndex()
        {
            if (nextWaveIndex == -1)
                InitWaveIndex();

            nextWaveIndex = Mathf.Clamp(nextWaveIndex + 1, 0, waves.Length - 1);
        }

        public void SaveWavesData()
        {
            ES3.Save(waveIndexSave, nextWaveIndex);
        }

        void InitWaveIndex()
        {
            nextWaveIndex = ES3.KeyExists(waveIndexSave) ?
                ES3.Load<int>(waveIndexSave) :
                0;
        }
        
#if UNITY_EDITOR
        [Button]
        void ResetWaveIndex()
        {
            nextWaveIndex = 0;
            ES3.Save(waveIndexSave, 0);
        }
        
        [Button]
        void DecreaseNextWaveIndex()
        {
            if (nextWaveIndex == -1)
                InitWaveIndex();
            
            nextWaveIndex = Mathf.Clamp(nextWaveIndex - 1, 0, waves.Length - 1);
        }
#endif
    }
}