using UnityEngine;

namespace Game.Characters.Spawners.Formations
{
    public interface ISpawnFormation
    {
        public Vector2[] GetSpawnPoints(Transform transform);
    }
}