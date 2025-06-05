using UnityEngine;
using Utilities.Attributes;

namespace Game.UI.BaseUiScope
{
    [RequireComponent(typeof(MoveAnimator))]
    public class BaseUiItem : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectTransform;

        [SerializeField]
        MoveAnimator moveAnimator;

        [SerializeField, AsEnum(typeof(UIElementIDList))]
        string itemId;

        public RectTransform RectTransform => rectTransform;
        public MoveAnimator MoveAnimator => moveAnimator;
        public string ItemId => itemId;

        public bool IsEnabled { get; set; } = true;
    }


}