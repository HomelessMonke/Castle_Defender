using UnityEngine;
using Utilities.Attributes;

namespace Game.UI.BaseUiScope
{
    [RequireComponent(typeof(UIAnimator))]
    public class BaseUiItem : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectTransform;

        [SerializeField]
        UIAnimator uiAnimator;

        [SerializeField, AsEnum(typeof(UIElementIDList))]
        string itemId;

        public RectTransform RectTransform => rectTransform;
        public UIAnimator UIAnimator => uiAnimator;
        public string ItemId => itemId;

        public bool IsEnabled { get; set; } = true;
    }


}