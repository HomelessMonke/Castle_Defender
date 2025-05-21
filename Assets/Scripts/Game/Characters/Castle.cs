using System;
using Game.Characters.Parameters;
using UI;
using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(Health))]
    public class Castle : MonoBehaviour
    {
        [SerializeField]
        CastleParameters parameters;
        
        [SerializeField]
        Health health;
        
        [SerializeField]
        Health[] hpAreas;
        
        [SerializeField]
        HealthView hpView;

        public Health[] HpAreas => hpAreas;
        public event Action Die;
        
        public void Init()
        {
            health.Init(parameters.HP);
            health.DamageTaken += (_)=> hpView.Draw(health);
            health.Died += OnDie;

            foreach (var hpArea in hpAreas)
            {
                hpArea.Init(1, true);
                hpArea.DamageTaken -= OnDamageablePartDamaged;
                hpArea.DamageTaken += OnDamageablePartDamaged;
            }
        }

        void OnDamageablePartDamaged(float damage)
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