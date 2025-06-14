using System;
using Game.Currencies;
using Game.Signals.AllyArcher;
using Game.Upgrades;
using UnityEngine;
using UnityEngine.Localization;
using Utilities.Attributes;

namespace Game.Grades.AllyArchersGrades
{
    /// <summary>
    /// Конфиг грейдов кол-ва союзных лучников
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/AllyArchers/(A)ArchersCount", fileName = "(A)ArchersCountUpgrades")]
    public class AllyArchersCountGrades: ParameterGrades
    {
        [SerializeField]
        int defaultCount = 4;
        
        [Header("Значение поля означает кол-во лучников на поле")]
        [SerializeField]
        Grade<int>[] grades;

        [SerializeField]
        LocalizedString localization;
        
        protected override string SaveKey => "AllyArchersCountGradeIndex";
        
        public override bool IsCompleted => gradeIndex == grades.Length-1;
        public override CurrencyItem CurrencyToUpgrade => grades[gradeIndex+1].Currency;
        public override string LocalizedDescription => String.Format(localization.GetLocalizedString(), GetCountForLocalization);
        
        int GetCountForLocalization => gradeIndex<0
            ? grades[gradeIndex+1].Value - defaultCount
            : grades[gradeIndex+1].Value - grades[gradeIndex].Value;

        public int ArchersCount => gradeIndex<0 ? defaultCount: grades[gradeIndex].Value;

        [Button(runtimeOnly:true)]
        public override void Upgrade()
        {
            var previousCount = gradeIndex<0? defaultCount : grades[gradeIndex].Value;
            gradeIndex = Mathf.Min(gradeIndex+1, grades.Length-1);
            signalBus.Fire(new AllyArchersCountUpgradeSignal(ArchersCount-previousCount));
        }
    }
}