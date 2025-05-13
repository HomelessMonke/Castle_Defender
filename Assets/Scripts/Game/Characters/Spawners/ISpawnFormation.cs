using UnityEngine;

namespace Game.Characters.Spawners
{
    public interface ISpawnFormation
    {
        public Vector2[] GetSpawnPoints(Vector2 startPosition);
    }
}