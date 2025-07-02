using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DarkPanel : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvasGroup;

        Tween fadeTween;

        public void TogglePanel(bool isActive)
        {
            if (fadeTween != null && fadeTween.IsActive())
                fadeTween.Kill();
            
            if (isActive)
            {
                gameObject.SetActive(true);
                fadeTween = DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.3f).SetUpdate(true);
            }
            else
            {
                fadeTween = DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 0.3f).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
            }
        }
        
    }
}
