using System.Linq;
using UnityEngine;

namespace Game.UI.BaseUiScope
{
    public class BaseUiSideAnimator : BaseUiAnimator
    {
        [SerializeField]
        Direction direction;

        [SerializeField]
        Vector2 offsetVector = new (0, 250);
        
        Vector2 rotatedOffsetVector;

        public override Direction Direction => direction;

        void Awake()
        {
            rotatedOffsetVector = Quaternion.AngleAxis(-(int)direction * 45, Vector3.forward) * offsetVector;
        }
        
        protected override void AnimateItem(BaseUiItem item, RectInfo oldRectInfo, RectInfo newRectInfo, float duration)
        {
            if (oldRectInfo.Enable && newRectInfo.Enable)
                item.UIAnimator.AnimateMove(oldRectInfo.Position, newRectInfo.Position, duration);

            if (!oldRectInfo.Enable && newRectInfo.Enable)
            {
                item.gameObject.SetActive(true);
                var startPos = newRectInfo.Position + rotatedOffsetVector;
                item.UIAnimator.AnimateShow(startPos, newRectInfo.Position, duration);
            }

            if (oldRectInfo.Enable && !newRectInfo.Enable)
            {
                item.gameObject.SetActive(true);
                var endPos = oldRectInfo.Position + rotatedOffsetVector;
                item.UIAnimator.AnimateHide(oldRectInfo.Position, endPos, duration, () => item.gameObject.SetActive(false));
            }

            if (!oldRectInfo.Enable && !newRectInfo.Enable)
                item.gameObject.SetActive(false);
        }
    }
}