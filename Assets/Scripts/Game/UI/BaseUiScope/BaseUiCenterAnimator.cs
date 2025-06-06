using UnityEngine;

namespace Game.UI.BaseUiScope
{
    public class BaseUiCenterAnimator : BaseUiAnimator
    {
        [SerializeField]
        Vector2 offsetVector = new (250, 0);
        
        public override Direction Direction => Direction.None;
        
        protected override void AnimateItem(BaseUiItem item, RectInfo oldRectInfo, RectInfo newRectInfo, float duration)
        {
            if (oldRectInfo.Enable && newRectInfo.Enable)
                item.UIAnimator.AnimateMove(oldRectInfo.Position, newRectInfo.Position, duration);

            if (!oldRectInfo.Enable && newRectInfo.Enable)
            {
                var startPos = newRectInfo.Position - offsetVector;
                item.RectTransform.position = startPos;
                item.UIAnimator.AnimateShow(startPos, newRectInfo.Position, duration);
            }

            if (oldRectInfo.Enable && !newRectInfo.Enable)
            {
                var endPos = oldRectInfo.Position + offsetVector;
                item.UIAnimator.AnimateHide(oldRectInfo.Position, endPos, duration, () =>
                {
                    item.gameObject.SetActive(false);
                });
            }

            if (!oldRectInfo.Enable && !newRectInfo.Enable)
                item.gameObject.SetActive(false);
        }
    }
}