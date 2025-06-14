using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Game.Characters
{
    public class GlideAnimator
    {
        public void Glide(Transform tf, TextMeshPro tmpText, SpriteRenderer icon, Action complete = null)
        {
            float angle = Random.Range(-45f, 45f);
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;
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