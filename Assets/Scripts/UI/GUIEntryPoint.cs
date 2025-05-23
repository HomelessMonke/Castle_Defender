using UI.Abilities;
using UI.Currencies;
using UnityEngine;

namespace UI
{
    public class GUIEntryPoint: MonoBehaviour
    {
        [SerializeField]
        CurrenciesPanel currenciesPanel;
        
        [SerializeField]
        AbilitiesPanel abilitiesPanel;

        public void Init()
        {
            abilitiesPanel.Init();
            currenciesPanel.DrawViews();
        }
    }
}