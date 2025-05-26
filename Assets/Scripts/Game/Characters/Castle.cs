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

        [SerializeField]
        UIHealthView hpView;
        
        [Header("Участки линии которые принимают урон")]
        [SerializeField]
        Health[] hpAreas;
        
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

            foreach (var hpArea in hpAreas)
            {
                hpArea.Init(1, true);
                hpArea.DamageTaken -= OnDamageTaken;
                hpArea.DamageTaken += OnDamageTaken;
            }
            
            hpView.Draw(health);
        }

        void OnHpUpgrade(CastleHealthUpgradeSignal signal)
        {
            health.SetHealth(parameters.HP);
            hpView.Draw(health);
        }

        void OnDamageTaken(float damage)
        {
            health.GetDamage(damage);
            hpView.Draw(health.CurrentHp, health.Percentage);
        }
        
        void OnDie()
        {
            Die?.Invoke();
        }
    }
}