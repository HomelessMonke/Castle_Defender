using System;
using System.Collections.Generic;
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

            popupLoaded?.Invoke(typedPopup);
            typedPopup.Show();
            return typedPopup;
        }

        public void ShowBlockOverlay()
        {
            overlay.SetActive(true);
        }
        
        public void HideBlockOverlay()
        {
            overlay.SetActive(false);
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