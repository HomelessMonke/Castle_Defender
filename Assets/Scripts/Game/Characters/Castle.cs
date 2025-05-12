using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Characters
{
    [RequireComponent(typeof(Health))]
    public class Castle : MonoBehaviour
    {
        [SerializeField]
        Health health;
        
        [SerializeField]
        Health[] hpAreas;
        
        [SerializeField]
        HealthView hpView;

        public Health[] HpAreas => hpAreas;
        public event Action Die;
        
        public void Init(int maxHealth)
        {
            health.Init(maxHealth);
            health.DamageTaken += (_)=> hpView.Draw(health);
            health.Died += OnDie;

            foreach (var hpArea in hpAreas)
            {
                hpArea.Init(1, true);
                hpArea.DamageTaken -= OnDamageablePartDamaged;
                hpArea.DamageTaken += OnDamageablePartDamaged;
            }
        }

        void OnDamageablePartDamaged(int damage)
        {
            health.GetDamage(damage);
        }

        void OnDie()
        {
            Die?.Invoke();
            hpView.SetActive(false);
        }
    }
}