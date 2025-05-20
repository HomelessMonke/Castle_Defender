using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Upgrades
{
    /// <summary>
    /// Список всех возможных улучшений для пользователя
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/CharacterUpgradesSequenceList", fileName = "CharacterUpgradesList")]
    public class CharacterGradesSequenceList: ScriptableObject
    {
        [SerializeField]
        CharacterGradesSequence[] upgradeSequences;

        SignalBus signalBus;

        public List<CharacterGradesSequence> GetNotCompletedSequences => upgradeSequences.Where(x => !x.IsCompleted).ToList();
        
        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            Debug.Log($"signalBus Installed in {name}");
        }
        
        public void Init()
        {
            foreach (var sequence in upgradeSequences)
            {
                sequence.Init(signalBus);
            }
        }
    }
}