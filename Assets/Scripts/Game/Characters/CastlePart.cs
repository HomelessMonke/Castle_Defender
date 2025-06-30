using System;
using Game.Characters.Units;
using UnityEngine;

namespace Game.Characters
{
    public class CastlePart : MonoBehaviour, IDamageable
    {
        int index;
        Health health;
        public string Id => $"CastlePart{index}";
        public bool IsAlive => health.IsAlive;
        public Transform Transform => transform;
        public Health HealthComponent => health;
        
        public void Init(int index, Health health)
        {
            this.index = index;
            this.health = health;
        }
    }
}