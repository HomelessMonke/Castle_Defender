using Game.Popups;
using Game.UI.BaseUiScope;
using Game.UI.Currencies;
using Game.UI.Popups;
using UI.Abilities;
using UnityEngine;

namespace Game.UI
{
    public class UIEntryPoint: MonoBehaviour
    {
        [SerializeField]
        BaseUI baseUI;
        
        [SerializeField]
        CurrenciesPanel currenciesPanel;
        
        [SerializeField]
        AbilitiesPanel abilitiesPanel;

        [SerializeField]
        PopupManager popupManager;
        
        public void Init()
        {
            baseUI.Init();
            abilitiesPanel.Init();
            currenciesPanel.DrawViews();
            popupManager.ConfigChanged -= OnPopupConfigChanged;
            popupManager.ConfigChanged += OnPopupConfigChanged;
        }

        void OnPopupConfigChanged(PopupConfig config)
        {
            baseUI.Switch(config);
        }
    }
}