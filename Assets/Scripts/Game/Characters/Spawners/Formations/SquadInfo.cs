using System;
using System.Linq;
using UnityEngine;

namespace Game.Characters.Spawners.Formations
{
    [Serializable]
    public class SquadInfo
    {
        [SerializeField]
        float spawnDelay;
        
        [SerializeField]
        EnemyType type;
        
        [Header("Сколько юнитов в каждой линии")]
        [SerializeField]
        int[] charactersInLines;

        [Header("Параметры построения")]
        [SerializeReference, SelectType]
        ISpawnFormation formation;
        
        public EnemyType Type => type;
        public float SpawnDelay => spawnDelay;
        
        public int Count => charactersInLines.Sum(x => x);
        
        public Vector2[] GetSpawnPoints(Transform transform)
        {
            return formation.GetSpawnPoints(charactersInLines, transform);
        }
    }
}