using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace Game.Grades
{
    /// <summary>
    /// Несколько параметров, объединённые в одну последовательность.
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/ParameterGradesSequence", fileName = "ParameterGradesSequence")]
    public class ParameterGradesSequence : ScriptableObject
    {
        [SerializeField]
        ParameterGrades[] parametersToUpgrade;

        [SerializeField]
        LocalizedString headerLocalization;
        
        public int Level => TotalUpgrades+1;
        public bool IsCompleted => parametersToUpgrade.All(x => x.IsCompleted);
        public string HeaderText => headerLocalization.GetLocalizedString(); 
        
        int TotalUpgrades => parametersToUpgrade.Sum(x => x.TotalUpgrades);
        
        public void Init(SignalBus signalBus)
        {
            foreach (var paramGrades in parametersToUpgrade)
            {
                paramGrades.Init(signalBus);
            }
        }

        public ParameterGrades GetParameterToUpgrade()
        {
            var lenght = parametersToUpgrade.Length;
            var totalUpgrades = TotalUpgrades;
            for (var i = 0; i < lenght; i++)
            {
                var curIndex = (totalUpgrades + i) % lenght;
                var upgrades = parametersToUpgrade[curIndex];
                if(!upgrades.IsCompleted)
                    return upgrades;
            }
            return null;
        }
    }
}