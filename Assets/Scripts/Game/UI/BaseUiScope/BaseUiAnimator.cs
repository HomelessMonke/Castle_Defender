using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.BaseUiScope
{
    public class BaseUiAnimator : MonoBehaviour
    {
        [SerializeField]
        Direction direction;

        [SerializeField]
        UIBehaviour layoutGroup;

        BaseUiItem[] currentItems;
        UiItemsList uiElementsList;
        
        Vector2 rotatedOffsetVector;

        public Direction Direction => direction;

        IEnumerator Start()
        {
            yield return null;
            layoutGroup.enabled = false;
        }

        public void Init(UiItemsList uiElementsList, Vector3 offsetVector)
        {
            this.uiElementsList = uiElementsList;
            rotatedOffsetVector = Quaternion.AngleAxis(-(int)direction * 45, Vector3.forward) * offsetVector;
            
            currentItems = transform.GetComponentsInChildren<BaseUiItem>().Where(x => x.gameObject.activeSelf).ToArray();
        }

        public void SetState(string[] itemsIdsToShow, float duration)
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

            var itemsToShow = uiElementsList.GetUiElementsArray(itemsIdsToShow);
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

        void RebuildLayout()
        {
            layoutGroup.enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            layoutGroup.enabled = false;
        }
        
        void AnimateItem(BaseUiItem item, RectInfo oldRectInfo, RectInfo newRectInfo, float duration)
        {
            if (oldRectInfo.Enable && newRectInfo.Enable)
                item.MoveAnimator.Move(oldRectInfo.Position, newRectInfo.Position, duration);

            if (!oldRectInfo.Enable && newRectInfo.Enable)
            {
                item.gameObject.SetActive(true);
                var startPos = newRectInfo.Position + rotatedOffsetVector;
                item.RectTransform.position = startPos;
                item.MoveAnimator.Move(startPos, newRectInfo.Position, duration);
            }

            if (oldRectInfo.Enable && !newRectInfo.Enable)
            {
                item.gameObject.SetActive(true);
                var endPos = oldRectInfo.Position + rotatedOffsetVector;
                var item1 = item;
                item.MoveAnimator.Move(oldRectInfo.Position, endPos, duration, () =>
                {
                    item1.gameObject.SetActive(false);
                });
            }

            if (!oldRectInfo.Enable && !newRectInfo.Enable)
                item.gameObject.SetActive(false);
        }
    }
}