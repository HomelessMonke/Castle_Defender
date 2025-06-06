using System;
using UnityEngine;
using Utilities.Attributes;

namespace Game.UI.BaseUiScope
{
    [Serializable]
    public class UIElementsGroup
    {
        [SerializeField]
        Direction direction;
		
        [SerializeField, AsEnum(typeof(UIElementIDList))]
        string[] visibleElements;
		
        public Direction Direction => direction;
		
        public string[] VisibleElements => visibleElements;

        public bool RemoveVisibleElement(string id)
        {
            if (visibleElements == null || visibleElements.Length == 0)
                return false;

            var indexToRemove = -1;
            for (int i = 0; i < visibleElements.Length; i++)
            {
                if (visibleElements[i] == id)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove != -1)
            {
                string[] newArray = new string[visibleElements.Length - 1];
                for (int i = 0, j = 0; i < visibleElements.Length; i++)
                {
                    if (i != indexToRemove)
                    {
                        newArray[j] = visibleElements[i];
                        j++;
                    }
                }
                visibleElements = newArray;
            }

            return indexToRemove != -1;
        }
    }
}