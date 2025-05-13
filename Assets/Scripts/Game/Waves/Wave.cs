using System.Collections.Generic;
using Game.Characters.Spawners.FormationSpawnParameters;
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

        public SquadInfo[] Squads => squads;

        [Button]
        public void AddSquad()
        {
            var list = new List<SquadInfo>(squads);
            list.Add(new SquadInfo());
            squads = list.ToArray();
            EditorUtility.SetDirty(this);
        }
    }
}