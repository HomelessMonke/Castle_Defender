using UI.Abilities;
using UI.Currencies;
using UnityEngine;

namespace UI
{
    public class UIEntryPoint: MonoBehaviour
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