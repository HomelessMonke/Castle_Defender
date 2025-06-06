using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UI.BaseUiScope
{
    [Serializable]
    public struct BaseUiItemArray
    {
        [SerializeField]
        BaseUiItem[] items;

        Dictionary<string, BaseUiItem> itemsCache;

        public BaseUiItem GetItemById(string id)
        {
            if (string.IsNullOrEmpty(id) || items == null || items.Length == 0)
                return null;

            if (itemsCache == null)
            {
                itemsCache = new Dictionary<string, BaseUiItem>();
                foreach (var item in items)
                {
                    if (item != null && !string.IsNullOrEmpty(item.ItemId))
                        itemsCache[item.ItemId] = item;
                }
            }

            return itemsCache.GetValueOrDefault(id);
        }

        public BaseUiItem[] GetUiElementsArray(string[] ids)
        {
            if (ids == null || ids.Length == 0 || items == null || items.Length == 0)
                return Array.Empty<BaseUiItem>();

            return Array.FindAll(items, item => item != null && ids.Contains(item.ItemId));
        }
        
#if UNITY_EDITOR
        public void SetItems(BaseUiItem[] newItems)
        {
            items = newItems;
            itemsCache = null;
        }
#endif
    }
}