using System;
using System.Collections.Generic;
using Game.UI.Popups;
using UnityEngine;

namespace Game.Popups
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField]
        Transform popupRoot;
        
        [SerializeField]
        GameObject overlay;

        readonly Dictionary<string, Popup> popupCache = new();

        public event Action<PopupConfig> ConfigChanged;

        public T OpenPopup<T>(string name, Action<T> popupLoaded = null) where T : Popup
        {
            if (!popupCache.TryGetValue(name, out var popup))
            {
                popup = LoadPopup(name);
                if (popup == null)
                {
                    Debug.LogError($"Popup '{name}' not found in Resources/Popups/");
                    return null;
                }

                popupCache[name] = popup;
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

        public void ShowOverlay()
        {
            overlay.SetActive(true);
        }
        
        public void HideOverlay()
        {
            overlay.SetActive(false);
        }

        void OnPopupClosed()
        {
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