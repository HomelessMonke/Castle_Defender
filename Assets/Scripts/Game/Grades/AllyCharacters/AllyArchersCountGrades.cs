using Game.Signals.AllyArcher;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Grades.AllyCharacters
{
    /// <summary>
    /// Конфиг грейдов кол-ва союзных лучников
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/AllyArchers/(A)ArchersCount", fileName = "(A)ArchersCountUpgrades")]
    public class AllyArchersCountGrades: AllyCharacterCountGrades
    {
        protected override string SaveKey => "AllyArchersCountGradeIndex";
        
        [Button(runtimeOnly:true)]
        public override void Upgrade()
        {
            var previousCount = gradeIndex<0? defaultCount : grades[gradeIndex].Value;
            gradeIndex = Mathf.Min(gradeIndex+1, grades.Length-1);
            signalBus.Fire(new AllyArchersCountUpgradeSignal(CharactersCount-previousCount));
        }
    }

}