using System;
using UnityEngine;

namespace Game.Waves
{

    [Serializable]
    public class Wave
    {
        [SerializeField]
        SquadInfo[] squads;
            
        [SerializeField]
        float nextWaveDelay;
        
        public float NextWaveDelay => nextWaveDelay;
        public SquadInfo[] Squads => squads;
    }
}