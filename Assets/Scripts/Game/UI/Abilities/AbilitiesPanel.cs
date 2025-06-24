using Game.Characters.Spawners;
using Game.Characters.Spawners.Formations;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Abilities
{
    public class AbilitiesPanel: MonoBehaviour
    {
        [SerializeField]
        Button callSoldiersButton;

        [SerializeField]
        AllyMeleeCharacterSpawner soldiersSpawner;

        [SerializeField]
        SquadInfo soldiersInfo;
        
        public void Init()
        {
            callSoldiersButton.gameObject.SetActive(false);
        }
    }
}