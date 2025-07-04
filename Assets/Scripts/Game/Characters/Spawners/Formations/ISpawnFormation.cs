using UnityEngine;

namespace Game.Characters.Spawners.Formations
{
    public interface ISpawnFormation
    {
        public bool SyncMovement { get;} 
        public Vector2[] GetSpawnPoints(int[] charactersInLines, Transform transform);
    }
}