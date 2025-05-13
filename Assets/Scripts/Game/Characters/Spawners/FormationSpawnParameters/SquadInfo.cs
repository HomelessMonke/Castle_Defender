using System;
using UnityEngine;

namespace Game.Characters.Spawners.FormationSpawnParameters
{
    [Serializable]
    public class SquadInfo
    {
        [SerializeField]
        float spawnDelay;
        
        [SerializeField]
        CharacterType type;

        [Header("Параметры построения")]
        [SerializeReference, SelectType]
        ISpawnFormation formation;
        
        public CharacterType Type => type;
        public float SpawnDelay => spawnDelay;
        
        public Vector2[] GetSpawnPoints(Vector2 startPosition)
        {
            return formation.GetSpawnPoints(startPosition);
        }
    }
}