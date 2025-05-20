using System;
using Game.Characters.Projectiles;
using Game.Characters.Spawners;
using UnityEngine;

namespace Game.Characters.Attacks
{
    public class RangedAttack: IAttack
    {
        float projectileSpeed;
        Transform projSpawnPos;
        ProjectileSpawner spawner;
        ProjectileAnimationData animationData;
        
        public RangedAttack(Transform projSpawnPos, ProjectileAnimationData animationData)
        {
            this.projSpawnPos = projSpawnPos;
            this.animationData = animationData;
        }

        public void Init(float projectileSpeed, ProjectileSpawner spawner)
        {
            this.spawner = spawner;
            this.projectileSpeed = projectileSpeed;
        }
        
        public void Attack(int damage, Health targetHP)
        {
            var projectile = spawner.Spawn(projSpawnPos.position);
            projectile.Launch(targetHP, animationData, damage, projectileSpeed);
        }
    }
}