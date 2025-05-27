using UnityEngine;

namespace Game.Popups
{
    public abstract class Popup: MonoBehaviour
    {
        public virtual void Show() => gameObject.SetActive(true);
        
        public virtual void Hide() => gameObject.SetActive(false);
    }
}