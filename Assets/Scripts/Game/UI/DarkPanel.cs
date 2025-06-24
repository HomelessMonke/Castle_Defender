using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DarkPanel : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvasGroup;

        public void TogglePanel(bool isActive)
        {
            if (isActive)
            {
                gameObject.SetActive(true);
                canvasGroup.DOFade(1f, 0.3f).SetUpdate(true);
            }
            else
                canvasGroup.DOFade(0f, 0.3f).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
        }
    }
}
