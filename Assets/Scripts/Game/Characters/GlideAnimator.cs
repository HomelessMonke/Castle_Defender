using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Game.Characters
{
    public class GlideAnimator
    {
        GlideAnimatorData data;
        
        public void Init(GlideAnimatorData data)
        {
            this.data = data;
        }
        
        public void Glide(Transform tf, TextMeshPro tmpText, SpriteRenderer icon, Action complete = null)
        {
            Vector3 dir = Quaternion.Euler(0, 0, data.GetRandomAngle()) * new Vector3(0, data.GlideHeight, 0);
            
            Sequence seq = DOTween.Sequence();
            seq.Append(tf.DOMove(tf.position + dir, .3f)
                .SetEase(Ease.OutQuad));

            seq.Append(tmpText.DOFade(0f, .3f)
                .SetEase(Ease.InQuad));

            seq.Join(icon.DOFade(0f, .3f)
                .SetEase(Ease.InQuad));
            
            seq.OnComplete(() => { complete?.Invoke(); });
        }
    }
}