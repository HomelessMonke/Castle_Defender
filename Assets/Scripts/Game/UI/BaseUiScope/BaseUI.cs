using System.Collections;
using Game.UI.Popups;
using UnityEngine;

namespace Game.UI.BaseUiScope
{
    public class BaseUI : MonoBehaviour
    {
        [SerializeField]
        UiItemsList items;

        [SerializeField]
        CanvasGroup canvasGroup;

        [SerializeField]
        PopupConfig inBattlePopupConfig;
        
        [SerializeField]
        PopupConfig preBattlePopupConfig;

        [SerializeField]
        BaseUiAnimator[] baseUiAnimators;

        [SerializeField]
        BaseUiAnimatorInfo animatorInfo;

        PopupConfig currentDefaultConfig;
        Coroutine coroutine;
        
        public void Init()
        {
            var offsetVector = animatorInfo.OffsetVector;
            foreach (var animator in baseUiAnimators)
            {
                animator.Init(items, offsetVector);
            }

            currentDefaultConfig = inBattlePopupConfig;
            InteractUiButtons(true);
        }
        
        public void SwitchInBattleConfig(bool immediate = false)
        {
            currentDefaultConfig = inBattlePopupConfig;
            Switch(inBattlePopupConfig, immediate);
        }

        public void SwitchPreBattleConfig(bool immediate = false)
        {
            currentDefaultConfig = preBattlePopupConfig;
            Switch(preBattlePopupConfig, immediate);
        }
        
        public void Hide(bool immediate = true)
        {
            Switch(new PopupConfig(), immediate);
        }

        public void Switch(PopupConfig config, bool immediate = false)
        {
            config ??= currentDefaultConfig;
            
            if (coroutine != null)
                StopCoroutine(coroutine);
            
            coroutine = StartCoroutine(SwitchCoroutine(config, immediate));
        }

        void InteractUiButtons(bool interactable)
        {
            canvasGroup.interactable = interactable;
        }

        void SwitchInternal(PopupConfig config, float duration)
        {
            foreach (var anim in baseUiAnimators)
            {
                var uiGroup = config.GetUIGroupByDirection(anim.Direction);
                var visibleElements = uiGroup != null ? uiGroup.VisibleElements : new string[] { };
                anim.SetState(visibleElements, duration);
            }
        }

        IEnumerator SwitchCoroutine(PopupConfig config, bool immediate = false)
        {
            InteractUiButtons(false);

            float duration = immediate ? 0 : animatorInfo.Duration;
            SwitchInternal(config, duration);
            
            yield return new WaitForSeconds(duration);
            InteractUiButtons(true);
        }
    }
}