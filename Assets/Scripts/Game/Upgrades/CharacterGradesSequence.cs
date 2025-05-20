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
        
        public int Level => parametersToUpgrade.Sum(x => x.Level);
        public bool IsCompleted => parametersToUpgrade.All(x => x.IsCompleted);

        public CharacterParameterGrades[] AllParameters => parametersToUpgrade;

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
            var level = Level;
            for (var i = 0; i < lenght; i++)
            {
                var curIndex = (level + i) % lenght;
                var upgrades = parametersToUpgrade[curIndex];
                if(!upgrades.IsCompleted)
                    return upgrades;
            }
            return null;
        }
    }
}