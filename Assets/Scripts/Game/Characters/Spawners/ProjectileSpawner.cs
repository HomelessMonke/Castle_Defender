using Game.Characters.Projectiles;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField]
        ObjectsPool<Projectile> pool;
        
        public void Init()
        {
            pool.Init();
        }
        
        public Projectile Spawn(Vector2 spawnPosition)
        {
            var projectile = pool.Spawn(true);
            projectile.transform.position = spawnPosition;
            projectile.Hit+= ()=> pool.Despawn(projectile);
            return projectile;
        }
    }
}