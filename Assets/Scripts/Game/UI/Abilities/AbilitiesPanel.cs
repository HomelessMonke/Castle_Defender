using Game.Characters.Spawners;
using Game.Characters.Spawners.Formations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Abilities
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
            //TODO: Должен бытьотдельный класс который контролирует кд спеллов и храниться значения солдат должны в сейве.
            callSoldiersButton.onClick.AddListener(()=> soldiersSpawner.Spawn(soldiersInfo));
        }
    }
}