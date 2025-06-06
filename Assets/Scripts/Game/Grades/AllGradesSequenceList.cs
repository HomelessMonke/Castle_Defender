using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Grades
{
    /// <summary>
    /// Список всех возможных улучшений для пользователя
    /// </summary>
    [CreateAssetMenu(menuName = "Upgrades/CharacterParameters/AllGradesSequenceList", fileName = "AllGradesSequenceList")]
    public class AllGradesSequenceList: ScriptableObject
    {
        [SerializeField]
        ParameterGradesSequence[] upgradeSequences;

        SignalBus signalBus;

        public IEnumerable<ParameterGradesSequence> GetNotCompletedSequences => upgradeSequences.Where(x => !x.IsCompleted).ToList();
        
        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
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