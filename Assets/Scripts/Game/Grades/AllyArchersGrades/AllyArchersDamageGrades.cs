﻿using System;
using Game.Currencies;
using Game.Signals.AllyArcher;
using Game.Upgrades;
using UnityEngine;
using UnityEngine.Localization;
using Utilities.Attributes;
using Zenject;

namespace Game.Grades.AllyArchersGrades
{
    /// <summary>
    /// Конфиг грейдов урона союзных лучников
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/AllyArchers/(A)ArchersDamage", fileName = "(A)ArchersDamageGrades")]
    public class AllyArchersDamageGrades: ParameterGrades
    {
        [SerializeField]
        float baseDamage;
        
        [Header("Проценты увеличения урона от baseDamage")]
        [SerializeField]
        Grade<float>[] grades;

        [SerializeField]
        LocalizedString localization;

        float currentDamage;
        
        protected override string SaveKey => "AllyArchersDamageGradeIndex";
        
        public override bool IsCompleted => gradeIndex == grades.Length-1;
        public override CurrencyItem CurrencyToUpgrade => grades[gradeIndex+1].Currency;
        public override string LocalizedDescription => String.Format(localization.GetLocalizedString(), grades[gradeIndex+1].Value);

        public float Damage => currentDamage;

        public override void Init(SignalBus signalBus)
        {
            base.Init(signalBus);
            UpdateDamageValue();
        }

        void UpdateDamageValue()
        {
            if (gradeIndex < 0)
            {
                currentDamage = baseDamage;
                return;
            }
            
            currentDamage = baseDamage * (1 + grades[gradeIndex].Value / 100f);
        }

        [Button(runtimeOnly:true)]
        public override void Upgrade()
        {
            gradeIndex = Mathf.Min(gradeIndex+1, grades.Length-1);
            UpdateDamageValue();
            signalBus.Fire(new AllyArchersDamageUpgradeSignal());
        }
    }
}