using System.Collections.Generic;
using System.Linq;
using Game.Characters.Spawners.Formations;
using Game.Currencies;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

namespace Game.Waves
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Waves/Wave")]
    public class Wave: ScriptableObject
    {
        [SerializeField]
        SquadInfo[] squads;

        [SerializeField]
        CurrencyItem rewardCurrency;
        
        public SquadInfo[] Squads => squads;
        public CurrencyItem RewardCurrency => rewardCurrency;

        public int CharactersCount => squads.Sum(x => x.Count);

#if UNITY_EDITOR
        [Button]
        public void AddSquad()
        {
            var list = new List<SquadInfo>(squads);
            list.Add(new SquadInfo());
            squads = list.ToArray();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}