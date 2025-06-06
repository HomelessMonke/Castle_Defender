using Game.Characters.Spawners;
using Game.Popups;
using Game.UI.BaseUiScope;
using Game.UI.Currencies;
using Game.UI.Popups;
using UI.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIEntryPoint: MonoBehaviour
    {
        [SerializeField]
        BaseUI baseUI;
        
        [SerializeField]
        WavesSpawner wavesManager;
        
        [SerializeField]
        CurrenciesPanel currenciesPanel;

        [SerializeField]
        Button startWaveButton;
        
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
            
            startWaveButton.onClick.AddListener(OnStartWaveClick);
        }

        void OnStartWaveClick()
        {
            baseUI.SwitchInBattleConfig();
            wavesManager.LaunchNextWave();
        }

        void OnPopupConfigChanged(PopupConfig config)
        {
            baseUI.Switch(config);
        }
    }
}