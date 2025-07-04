﻿using Game.Signals.AllyArcher;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Grades.AllyCharacters
{
    /// <summary>
    /// Конфиг грейдов урона союзных лучников
    /// </summary>
    public class AllyArchersDamageGrades: AllyCharacterDamageGrades
    {
        protected override string SaveKey => "AllyArchersDamageGradeIndex";

        [Button(runtimeOnly:true)]
        public override void Upgrade()
        {
            gradeIndex = Mathf.Min(gradeIndex+1, grades.Length-1);
            UpdateDamageValue();
            signalBus.Fire(new AllyArchersDamageUpgradeSignal());
        }
    }
}