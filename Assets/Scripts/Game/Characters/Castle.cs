using System;
using Game.Characters.Parameters;
using Game.Signals.Castle;
using Game.UI;
using UnityEngine;
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
        
        [Header("Участки линии которые принимают урон")]
        [SerializeField]
        CastlePart[] parts;
        
        [SerializeField]
        Health health;

        [SerializeField]
        UIHealthView hpView;
        
        public event Action Die;

        SignalBus signalBus;
        
        [Inject]
        void Construct(SignalBus signalBus)
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
            health.Died -= OnDie;
            health.Died += OnDie;
            health.DamageTaken -= OnDamageTaken;
            health.DamageTaken += OnDamageTaken;

            for (int i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                part.Init(i, health);
            }
            
            hpView.Draw(health);
        }

        public void RestoreHealth()
        {
            health.RestoreHealth();
            hpView.Draw(health);
        }

        void OnHpUpgrade(CastleHealthUpgradeSignal signal)
        {
            health.SetHealth(parameters.HP);
            hpView.Draw(health);
        }

        void OnDamageTaken(float damage)
        {
            hpView.Draw(health.CurrentHp, health.Percentage);
        }
        
        void OnDie()
        {
            Die?.Invoke();
        }
    }
}