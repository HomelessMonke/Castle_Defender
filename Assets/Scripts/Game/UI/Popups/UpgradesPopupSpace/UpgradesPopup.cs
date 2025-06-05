using System;
using Game.Grades;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups.UpgradesPopupSpace
{
    public class UpgradesPopup : Popup
    {
        [SerializeField]
        Transform root;
        
        [SerializeField]
        UpgradeView viewTemplate;

        [SerializeField]
        Button closeButton;

        public event Action<UpgradeView, ParameterGradesSequence> BuyClick;
        public event Action CloseClick;
        
        public void Init()
        {
            closeButton.onClick.AddListener(()=> CloseClick?.Invoke());
        }

        public void Draw(AllGradesSequenceList gradesSequenceList)
        {
            var sequences = gradesSequenceList.GetNotCompletedSequences;
            foreach (var sequence in sequences)
            {
                var parameterUpgrade = sequence.GetParameterToUpgrade();
                if(!parameterUpgrade)
                    continue;
                
                var view = Instantiate(viewTemplate, root);
                view.BuyClick += ()=> BuyClick?.Invoke(view, sequence);
                view.Init();
                view.Draw(sequence.HeaderText, parameterUpgrade);
            }
        }
    }
}