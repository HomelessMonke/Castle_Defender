using System;
using UnityEngine;

namespace Game.UI.Popups
{
    public abstract class Popup: MonoBehaviour
    {
        [SerializeField]
        PopupConfig config;
        
        public PopupConfig Config => config;

        public event Action Closed;
        
        public virtual void Open() => gameObject.SetActive(true);
        
        public virtual void Close()
        {
            gameObject.SetActive(false);
            Closed?.Invoke();
        }
    }

}