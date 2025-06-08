using System;
using TMPro;
using UnityEngine;

namespace Game.Characters
{
    public class LootBubble : MonoBehaviour
    {
        [SerializeField]
        TextMeshPro tmpText;

        [SerializeField]
        SpriteRenderer icon;

        GlideAnimator glideAnimator;
        
        public void Draw(int value)
        {
            tmpText.text = $"+{value}";

            ResetGraphicAlpha();
        }

        void ResetGraphicAlpha()
        {
            var color = icon.color;
            color.a = 1f;
            icon.color = color;

            tmpText.alpha = 1f;
        }

        public void Glide(Action complete = null)
        {
            glideAnimator ??= new();
            glideAnimator.Glide(transform, tmpText, icon, complete);
        }
    }
}
