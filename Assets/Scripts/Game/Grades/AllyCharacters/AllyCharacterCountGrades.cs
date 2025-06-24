using System;
using Game.Currencies;
using Game.Upgrades;
using UnityEngine;
using UnityEngine.Localization;

namespace Game.Grades.AllyCharacters
{
    [CreateAssetMenu]
    public abstract class AllyCharacterCountGrades: ParameterGrades
    {
        [SerializeField]
        protected int defaultCount;
        
        [Header("Значение поля означает кол-во юнитов на поле")]
        [SerializeField]
        protected Grade<int>[] grades;

        [SerializeField]
        LocalizedString localization;
        
        public override bool IsCompleted => gradeIndex == grades.Length-1;
        public override CurrencyItem CurrencyToUpgrade => grades[gradeIndex+1].Currency;
        public override string LocalizedDescription => String.Format(localization.GetLocalizedString(), GetCountForLocalization);
        
        int GetCountForLocalization => gradeIndex<0
            ? grades[gradeIndex+1].Value - defaultCount
            : grades[gradeIndex+1].Value - grades[gradeIndex].Value;

        public int CharactersCount => gradeIndex<0 ? defaultCount: grades[gradeIndex].Value;
    }
}