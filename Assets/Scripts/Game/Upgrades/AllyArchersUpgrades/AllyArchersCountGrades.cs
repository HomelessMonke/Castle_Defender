using System;
using Game.Signals.AllyArcher;
using UnityEngine;
using UnityEngine.Localization;
using Utilities.Attributes;
using Zenject;

namespace Game.Upgrades.AllyArchersUpgrades
{
    /// <summary>
    /// Конфиг грейдов кол-ва союзных лучников
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/AllyArchers/AllyArchersCount", fileName = "AllyArchersCountUpgrades")]
    public class AllyArchersCountGrades: CharacterParameterGrades
    {
        [Header("Значение поля означает кол-во лучников на поле")]
        [SerializeField]
        IntGrade[] grades;

        [SerializeField]
        Sprite sprite;

        [SerializeField]
        LocalizedString localization;
        
        const string SaveKey = "AllyArchersCountLevel";

        int level;
        bool inited;
        int archersCount;

        SignalBus signalBus;
        
        public override bool IsCompleted => level >= grades.Length-1;
        public override int Level => level;
        public override Sprite Sprite => sprite;

        public override string LocalizedDescription => String.Format(localization.GetLocalizedString(), GetCountForLocalization);
        int GetCountForLocalization => grades[level+1].Value - grades[level].Value;

        public int ArchersCount => archersCount;
        
        public override void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;

            level = ES3.KeyExists(SaveKey)
                ? ES3.Load<int>(SaveKey)
                : 0;
            
            archersCount = grades[level].Value;
        }

        [Button(runtimeOnly:true)]
        public override void Upgrade()
        {
            level = Mathf.Min(level+1, grades.Length-1);
            var previousCount = archersCount;
            archersCount = grades[level].Value;
            signalBus.Fire(new AllyArchersCountSignal(archersCount-previousCount));
        }
    }
}