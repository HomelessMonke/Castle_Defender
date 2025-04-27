using Game.Characters.Spawners;
using Game.Waves;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerAbilitiesUI: MonoBehaviour
    {
        [SerializeField]
        Button callSoldiersButton;

        [SerializeField]
        CharacterSpawner soldiersSpawner;

        [SerializeField]
        SquadInfo soldiersInfo;
        
        public void Init()
        {
            //TODO: Должен бытьотдельный класс который контролирует кд спеллов и храниться значения солдат должны в сейве.
            callSoldiersButton.onClick.AddListener(()=> soldiersSpawner.Spawn(soldiersInfo));
        }
    }
}