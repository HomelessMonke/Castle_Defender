using System;
using Game.Currencies;
using Game.Signals.Castle;
using Game.Upgrades;
using UnityEngine;
using UnityEngine.Localization;
using Utilities.Attributes;

namespace Game.Grades.CastleGrades
{
    /// <summary>
    /// Конфиг грейдов здоровья замка
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CastleHealthGrades", fileName = "CastleHealthGrades")]
    public class CastleHealthGrades: ParameterGrades
    {
        [SerializeField]
        float defaultHp = 50;
        
        [Header("Value это новое HP")]
        [SerializeField]
        Grade<float>[] grades;

        [SerializeField]
        LocalizedString localization;
        
        protected override string SaveKey => "CastleHpGradeIndex";
        
        public override bool IsCompleted => gradeIndex == grades.Length-1;
        public override CurrencyItem CurrencyToUpgrade => grades[gradeIndex].Currency;

        public override string LocalizedDescription => String.Format(localization.GetLocalizedString(), GetValueForLocalization);
        float GetValueForLocalization => gradeIndex<0
            ? grades[gradeIndex+1].Value - defaultHp
            : grades[gradeIndex+1].Value - grades[gradeIndex].Value;

        public float HP => gradeIndex<0 ? defaultHp: grades[gradeIndex].Value;

        [Button(runtimeOnly:true)]
        public override void Upgrade()
        {
            gradeIndex = Mathf.Min(gradeIndex+1, grades.Length-1);
            signalBus.Fire(new CastleHealthUpgradeSignal());
        }
    }
}