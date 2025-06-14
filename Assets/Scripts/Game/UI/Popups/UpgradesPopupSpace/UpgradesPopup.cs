using System;
using System.Collections.Generic;
using System.Linq;
using Game.Grades;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Extensions;

namespace Game.UI.Popups.UpgradesPopupSpace
{
    public class UpgradesPopup : Popup
    {
        [SerializeField]
        Transform root;
        
        [SerializeField]
        UpgradeView viewTemplate;

        [SerializeField]
        LayoutGroup layoutGroup;
        
        [SerializeField]
        Button closeButton;
        
        public event Action<UpgradeView, ParameterGradesSequence> BuyClick;
        public event Action CloseClick;
        
        public void Init()
        {
            closeButton.onClick.AddListener(()=> CloseClick?.Invoke());
        }

        public void Draw(ParameterGradesSequence[] sequences)
        {
            root.DestroyChildren();
            foreach (var sequence in sequences)
            {
                var parameterUpgrade = sequence.GetParameterToUpgrade();
                if(!parameterUpgrade)
                    continue;
                
                var view = Instantiate(viewTemplate, root);
                var seq = sequence;
                view.BuyClick += ()=> BuyClick?.Invoke(view, seq);
                view.Init();
                view.gameObject.SetActive(true);
                view.Draw(parameterUpgrade, sequence.Level, sequence.HeaderText);
            }
            viewTemplate.gameObject.SetActive(false);
        }
    }
}