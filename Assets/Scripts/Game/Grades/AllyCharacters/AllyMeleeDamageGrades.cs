using Game.Signals.AllyMelee;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Grades.AllyCharacters
{
    /// <summary>
    /// Конфиг грейдов урона союзных мечников
    /// </summary>
    public class AllyMeleeDamageGrades: AllyCharacterDamageGrades
    {
        protected override string SaveKey => "AllyMeleeDamageGradesIndex";

        [Button(runtimeOnly:true)]
        public override void Upgrade()
        {
            gradeIndex = Mathf.Min(gradeIndex+1, grades.Length-1);
            UpdateDamageValue();
            signalBus.Fire(new AllyMeleeDamageUpgradeSignal());
        }
    }
}