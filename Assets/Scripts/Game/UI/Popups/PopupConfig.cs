using System;
using System.Collections.Generic;
using System.Linq;
using Game.UI.BaseUiScope;
using UnityEngine;

namespace Game.UI.Popups
{
    [Serializable]
    public class PopupConfig
    {
        [SerializeField]
        List<UIElementsGroup> uiGroups = new ();
        
        public List<UIElementsGroup> UiGroups => uiGroups;
		
        public UIElementsGroup GetUIGroupByDirection(Direction direction) => uiGroups?.FirstOrDefault(x => x.Direction == direction);

        public void RemoveVisibleElement(string id)
        {
            if (uiGroups == null)
                return;

            foreach (var group in uiGroups)
                group.RemoveVisibleElement(id);
        }
    }

}