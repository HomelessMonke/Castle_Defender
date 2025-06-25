using System;
using Game.Currencies;
using Game.Signals.AllyMelee;
using Game.Upgrades;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace Game.Grades.AllyCharacters
{
    [CreateAssetMenu(menuName = "Grades/AllyMeleeHealthGrades", fileName = "AllyMeleeHealthGrades")]
    public class AllyMeleeHealthGrades: ParameterGrades
    {
        [SerializeField]
        float defaultValue;
        
        [Header("Значение поля это кол-во в процентах от defaultValue")]
        [SerializeField]
        Grade<float>[] grades;

        [SerializeField]
        LocalizedString localization;

        float healthPoints;
        
        protected override string SaveKey => "AllyMeleeHealthGradesIndex";
        public override bool IsCompleted => gradeIndex == grades.Length-1;
        public override CurrencyItem CurrencyToUpgrade => grades[gradeIndex+1].Currency;
        public override string LocalizedDescription => String.Format(localization.GetLocalizedString(), grades[gradeIndex+1].Value);
        
        public float HealthPoints => healthPoints;

        public override void Init(SignalBus signalBus)
        {
            base.Init(signalBus);
            UpdateHealthValue();
        }

        void UpdateHealthValue()
        {
            if (gradeIndex < 0)
            {
                healthPoints = defaultValue;
                return;
            }
            
            healthPoints = defaultValue * (1 + grades[gradeIndex].Value / 100f);
        }
        
        public override void Upgrade()
        {
            gradeIndex = Mathf.Min(gradeIndex+1, grades.Length-1);
            UpdateHealthValue();
            signalBus.Fire<AllyMeleeHealthUpgradeSignal>();
        }
    }
}