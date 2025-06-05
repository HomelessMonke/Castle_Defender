using System;
using UnityEngine;

namespace Game.UI.BaseUiScope
{
    public class MoveAnimator : MonoBehaviour
    {
        [SerializeField]
        AnimationCurveObject curveObj;

        float duration = -1;
        float timeStart;
        Vector3 startPos;
        Vector3 targetPos;
        Action finishCallback;

        float NormalizedTime => Mathf.Clamp01((Time.time - timeStart) / duration);

        void Start()
        {
            if (duration < 0)
                enabled = false;
        }

        public void Move(Vector3 startPos, Vector3 targetPos, float duration, Action finishCallback = null)
        {
            enabled              = true;
            transform.position   = startPos;
            this.startPos = startPos;
            this.targetPos  = targetPos;
            this.duration        = duration;
            this.finishCallback  = finishCallback;
            timeStart            = Time.time;

            if (Mathf.Approximately(duration, 0))
            {
                timeStart = 0;
                MoveToEndImmediate();
            }
        }

        void Update()
        {
            transform.position = Vector3.Lerp(startPos, targetPos, curveObj.Curve.Evaluate(NormalizedTime));
            if (Mathf.Approximately(NormalizedTime, 1))
                MoveToEndImmediate();
        }

        void MoveToEndImmediate()
        {
            transform.position = targetPos;
            enabled            = false;
            finishCallback?.Invoke();
        }
    }
}