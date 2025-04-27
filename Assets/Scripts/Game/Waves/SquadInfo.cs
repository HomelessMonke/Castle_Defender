using System;
using Game.Characters;
using UnityEngine;

namespace Game.Waves
{

    [Serializable]
    public class SquadInfo
    {
        [SerializeField]
        CharacterType type;

        [SerializeField]
        int maxUnitsInRow = 1;
        
        [SerializeField]
        int count;

        [SerializeField]
        float spawnDelay;
        
        public CharacterType Type => type;
        public float SpawnDelay => spawnDelay;
        public int Count => count;
        public int MaxUnitsInRow => maxUnitsInRow;
    }
}