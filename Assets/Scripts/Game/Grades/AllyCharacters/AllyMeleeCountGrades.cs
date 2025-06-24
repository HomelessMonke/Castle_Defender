using Game.Signals.AllyMelee;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Grades.AllyCharacters
{
    /// <summary>
    /// Конфиг грейдов кол-ва союзных юнитов ближнего боя
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/AllyMelee/CountGrades", fileName = "AllyMeleeCountGrades")]
    public class AllyMeleeCountGrades: AllyCharacterCountGrades
    {
        protected override string SaveKey => "AllyMeleeCountGradeIndex";
        
        [Button(runtimeOnly:true)]
        public override void Upgrade()
        {
            var previousCount = gradeIndex<0? defaultCount : grades[gradeIndex].Value;
            gradeIndex = Mathf.Min(gradeIndex+1, grades.Length-1);
            signalBus.Fire(new AllyMeleeCountUpgradeSignal(CharactersCount-previousCount));
        }
    }
}