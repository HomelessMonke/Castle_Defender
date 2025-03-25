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
        CharacterParameters parameters;

        [SerializeField]
        int count;

        [SerializeField]
        float spawnDelay;
        
        public CharacterType Type => type;
        public CharacterParameters Parameters => parameters;
        public float SpawnDelay => spawnDelay;
        public int Count => count;
    }
}