using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/CharacterUpgradesSequence", fileName = "CharacterUpgradesSequence")]
    public class CharacterGradesSequence : ScriptableObject
    {
        [SerializeField]
        CharacterParameterGrades[] parametersToUpgrade;
        
        public bool IsCompleted => parametersToUpgrade.All(x => x.IsCompleted);
        int TotalUpgrades => parametersToUpgrade.Sum(x => x.TotalUpgrades);
        
        public void Init(SignalBus signalBus)
        {
            foreach (var paramGrades in parametersToUpgrade)
            {
                paramGrades.Init(signalBus);
            }
        }

        public CharacterParameterGrades TryGetNextToUpgrade()
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