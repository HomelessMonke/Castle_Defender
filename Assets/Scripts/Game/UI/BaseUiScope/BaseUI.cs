using System.Collections;
using Game.UI.Popups;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

namespace Game.UI.BaseUiScope
{
    public class BaseUI : MonoBehaviour
    {
        [SerializeField]
        BaseUiItemArray itemsArray;

        [SerializeField]
        CanvasGroup canvasGroup;

        [SerializeField]
        PopupConfig inBattlePopupConfig;
        
        [SerializeField]
        PopupConfig preBattlePopupConfig;

        [SerializeField]
        BaseUiAnimator[] animators;

        [SerializeField]
        float animateDuration;

        PopupConfig currentDefaultConfig;
        Coroutine coroutine;
        
        public void Init()
        {
            SwitchPreBattleConfig(true);
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

        [Button]
        public void Hide()
        {
            Hide(false);
        }
        
        [Button]
        public void SwitchPreBattleConfig()
        {
            SwitchPreBattleConfig(false);
        }
        
        [Button]
        public void SwitchInBattleConfig()
        {
            SwitchInBattleConfig(false);
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

        IEnumerator SwitchCoroutine(PopupConfig config, bool immediate = false)
        {
            InteractUiButtons(false);

            float duration = immediate ? 0 : animateDuration;
            SwitchInternal(config, duration);
            
            yield return new WaitForSeconds(duration);
            InteractUiButtons(true);
        }
        
        void SwitchInternal(PopupConfig config, float duration)
        {
            foreach (var anim in animators)
            {
                var uiGroup = config.GetUIGroupByDirection(anim.Direction);
                var visibleElements = uiGroup != null ? uiGroup.VisibleElements : new string[] { };
                var itemsToShow = itemsArray.GetUiElementsArray(visibleElements);
                anim.SetState(itemsToShow, duration);
            }
        }
        
#if UNITY_EDITOR
        [Button]
        void SetAllChildrenItems()
        {
            var items = GetComponentsInChildren<BaseUiItem>();
            itemsArray.SetItems(items);
            EditorUtility.SetDirty(this);
        }
        
        [Button]
        void SetAllChildrenAnimators()
        {
            animators = GetComponentsInChildren<BaseUiAnimator>();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}