using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UI.BaseUiScope
{
    [Serializable]
    public struct UiItemsList
    {
        [SerializeField]
        BaseUiItem[] itemsList;

        public BaseUiItem GetItemById(string name)
        {
            return itemsList?.FirstOrDefault(x => x.ItemId.Equals(name));
        }

        public BaseUiItem[] GetUiElementsArray(string[] ids)
        {
            List<BaseUiItem> list = new List<BaseUiItem>();
            foreach (var id in ids)
            {
                var item = GetItemById(id);
                if (item)
                {
                    list.Add(item);
                }
            }
            
            return list.ToArray();
        }
    }
}