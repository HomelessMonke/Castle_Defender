using System;
using Game.Characters.Projectiles;
using Game.Characters.Spawners;
using UnityEngine;

namespace Game.Characters.Attacks
{
    public class RangedAttack: IAttack
    {
        float speed;
        Transform projSpawnPos;
        ProjectileSpawner spawner;
        ProjectileAnimationData animationData;
        
        public event Action AttackCompleted;

        public RangedAttack(Transform projSpawnPos, ProjectileAnimationData animationData)
        {
            this.projSpawnPos = projSpawnPos;
            this.animationData = animationData;
        }

        public void Init(float speed, ProjectileSpawner spawner)
        {
            this.spawner = spawner;
            this.speed = speed;
        }
        
        public void Attack(int damage, HealthComponent targetHP)
        {
            var projectile = spawner.Spawn(projSpawnPos.position);
            projectile.Launch(targetHP, animationData, damage, speed);
        }
    }
}