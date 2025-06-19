using System;
using Game.Currencies;
using Game.Upgrades;
using UnityEngine;
using Zenject;

namespace Game.Grades
{
    [Serializable]
    public abstract class ParameterGrades : ScriptableObject, IUpgrade
    {
        [SerializeField]
        Sprite sprite;
        
        protected int gradeIndex;
        protected SignalBus signalBus;
        
        public Sprite Sprite => sprite;
        public int TotalUpgrades => gradeIndex<0 ? 0 : gradeIndex + 1;
        
        protected abstract string SaveKey { get; }
        public abstract bool IsCompleted { get; }
        public abstract string LocalizedDescription { get; }
        public abstract CurrencyItem CurrencyToUpgrade { get; }
        
        public virtual void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;

            gradeIndex = ES3.KeyExists(SaveKey)
                ? ES3.Load<int>(SaveKey)
                : -1;
        }
        
        public abstract void Upgrade();
        
        public void Save()
        {
            ES3.Save(SaveKey, gradeIndex);
        }
    }
}