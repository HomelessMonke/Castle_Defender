using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.BaseUiScope
{
    public abstract class BaseUiAnimator : MonoBehaviour
    {
        [SerializeField]
        protected UIBehaviour layoutGroup;
        
        protected BaseUiItem[] currentItems;
        
        public abstract Direction Direction { get; }

        void Awake()
        {
            currentItems = transform.GetComponentsInChildren<BaseUiItem>().Where(x => x.gameObject.activeSelf).ToArray();
        }
        
        IEnumerator Start()
        {
            yield return null;
            layoutGroup.enabled = false;
        }
        
        public void SetState(BaseUiItem[] itemsToShow, float duration)
        {
            var allUniqueElements = new List<BaseUiItem>();
            Dictionary<BaseUiItem, RectInfo> oldItemsInfo = new Dictionary<BaseUiItem, RectInfo>();
            Dictionary<BaseUiItem, RectInfo> newItemsInfo = new Dictionary<BaseUiItem, RectInfo>();
            
            foreach (var element in currentItems)
            {
                allUniqueElements.Add(element);
                oldItemsInfo.Add(element, new RectInfo(element));
                element.gameObject.SetActive(false);
            }
            
            for (int i = 0; i < itemsToShow.Length; i++)
            {
                var item = itemsToShow[i];
                item.transform.SetSiblingIndex(i);
                if (!currentItems.Contains(item))
                {
                    item.transform.SetParent(transform);
                }
                item.gameObject.SetActive(true);

                if (!allUniqueElements.Contains(item))
                    allUniqueElements.Add(item);
            }

            RebuildLayout();
            
            foreach (var item in itemsToShow)
            {
                newItemsInfo.Add(item, new RectInfo(item));
            }

            foreach (var item in allUniqueElements)
            {
                var oldRectInfo = oldItemsInfo.TryGetValue(item, out var oldStateInfo) ? oldStateInfo : new RectInfo();
                var newRectInfo = newItemsInfo.TryGetValue(item, out var newStateInfo) ? newStateInfo : new RectInfo();
                AnimateItem(item, oldRectInfo, newRectInfo, duration);
            }

            currentItems = itemsToShow;
        }
        
        protected abstract void AnimateItem(BaseUiItem item, RectInfo oldRectInfo, RectInfo newRectInfo, float duration);
        
        void RebuildLayout()
        {
            layoutGroup.enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            layoutGroup.enabled = false;
        }
    }
}