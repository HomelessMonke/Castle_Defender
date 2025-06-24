using System;
using Game.UI;
using Game.UI.Popups;
using UnityEngine;

namespace Game.Popups
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField]
        Transform popupRoot;

        [SerializeField]
        DarkPanel darkPanel;
        
        public event Action<PopupConfig> ConfigChanged;

        public T OpenPopup<T>(string name, Action<T> popupLoaded = null) where T : Popup
        {
            var popup = LoadPopup(name);
            if (popup == null)
            {
                Debug.LogError($"Popup '{name}' not found in Resources/Popups/");
                return null;
            }

            if (popup is not T typedPopup)
            {
                Debug.LogError($"Popup '{name}' is not of type {typeof(T)}");
                return null;
            }

            popup.Closed -= OnPopupClosed;
            popup.Closed += OnPopupClosed;
            ConfigChanged?.Invoke(popup.Config);

            popupLoaded?.Invoke(typedPopup);
            typedPopup.Open();
            return typedPopup;
        }

        public void ShowDarkPanel()
        {
            darkPanel.TogglePanel(true);
        }
        
        public void HideDarkPanel()
        {
            darkPanel.TogglePanel(false);
        }

        void OnPopupClosed(Popup popup)
        {
            Destroy(popup.gameObject);
            ConfigChanged?.Invoke(null);
        }

        Popup LoadPopup(string name)
        {
            var prefab = Resources.Load<Popup>($"Popups/{name}");
            if (prefab == null)
                return null;

            var popup = Instantiate(prefab, popupRoot);
            popup.gameObject.SetActive(false);
            return popup;
        }
    }
}