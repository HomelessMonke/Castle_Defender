using System;
using Game.Currencies;
using Game.Upgrades;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace Game.Grades.AllyCharacters
{
    public abstract class AllyCharacterDamageGrades : ParameterGrades
    {
        [SerializeField]
        float baseDamage;
        
        [Header("Проценты увеличения урона от baseDamage")]
        [SerializeField]
        protected Grade<float>[] grades;

        [SerializeField]
        LocalizedString localization;
        
        float currentDamage;
        
        public override bool IsCompleted => gradeIndex == grades.Length-1;
        public override CurrencyItem CurrencyToUpgrade => grades[gradeIndex+1].Currency;
        public override string LocalizedDescription => String.Format(localization.GetLocalizedString(), grades[gradeIndex+1].Value);

        public float Damage => currentDamage;

        public override void Init(SignalBus signalBus)
        {
            base.Init(signalBus);
            UpdateDamageValue();
        }

        protected void UpdateDamageValue()
        {
            if (gradeIndex < 0)
            {
                currentDamage = baseDamage;
                return;
            }
            
            currentDamage = baseDamage * (1 + grades[gradeIndex].Value / 100f);
        }
    }
}