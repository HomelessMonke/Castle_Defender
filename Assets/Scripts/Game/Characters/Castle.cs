using System;
using Game.Characters.Parameters;
using Game.Signals.Castle;
using UI;
using UnityEngine;
using Utilities.Attributes;
using Zenject;

namespace Game.Characters
{
    /// <summary>
    /// Главный объект, который нужно защищать
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class Castle : MonoBehaviour
    {
        [SerializeField]
        CastleParameters parameters;
        
        [SerializeField]
        Health health;
        
        [Header("Участки линии которые принимают урон")]
        [SerializeField]
        Health[] hpAreas;
        
        [SerializeField]
        HealthView hpView;

        public event Action Die;

        SignalBus signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        void Start()
        {
            signalBus.Subscribe<CastleHealthUpgradeSignal>(OnHpUpgrade);
        }

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

        void OnHpUpgrade(CastleHealthUpgradeSignal signal)
        {
            health.SetHealth(parameters.HP);
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

        [Button]
        void LogCurrentHP()
        {
            Debug.Log(health.CurrentHealth);
        }
    }
}