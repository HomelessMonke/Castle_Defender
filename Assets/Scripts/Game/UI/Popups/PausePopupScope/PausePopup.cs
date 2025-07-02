using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups.PausePopupScope
{
    public class PausePopup : Popup
    {
        [SerializeField]
        Button resumeButton;

        public event Action ResumeClick;
        
        public void Init()
        {
            resumeButton.onClick.AddListener(() => ResumeClick?.Invoke());
        }
    }
}
