using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game.Characters
{
    public class DamageFlash: MonoBehaviour
    {
        [SerializeField]
        float duration;
        
        [SerializeField]
        Color flashColor;

        [SerializeField]
        AnimationCurve animationCurve;
        
        [SerializeField]
        SpriteRenderer[] spriteRenderers;
        
        Material[] materials;
        Tween flashTween;
        
        void Start()
        {
            if (spriteRenderers.Length > 0)
            {
                materials = spriteRenderers.Select(x=> x.material).ToArray();
                foreach (var material in materials)
                {
                    material.SetColor("_FlashColor", flashColor);
                }
            }
        }

        public void Flash()
        {
            if (flashTween != null)
            {
                flashTween.Restart();
                return;
            }
            
            flashTween = DOTween.To(() => 0, x =>
            {
                var amount = animationCurve.Evaluate(x);
                SetMaterialFlashAmount(amount);
            }, 1, duration).SetAutoKill(false);
        }
        

        void SetMaterialFlashAmount(float flashAmount)
        {
            foreach (var material in materials)
            {
                material.SetFloat("_FlashAmount", flashAmount);
            }
        }

        void OnDisable()
        {
            SetMaterialFlashAmount(0);
        }
        
        void OnDestroy()
        {
            DOTween.Kill(flashTween);
        }
    }
}