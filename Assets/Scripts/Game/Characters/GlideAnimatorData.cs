using UnityEngine;

namespace Game.Characters
{
    public class GlideAnimatorData : ScriptableObject
    {
        [SerializeField]
        float minAngle, maxAngle;

        [SerializeField]
        float glideHeight;

        public float MinAngle => minAngle;

        public float MaxAngle => maxAngle;

        public float GlideHeight => glideHeight;
        
        public float GetRandomAngle()
        {
            var angle = Random.Range(minAngle, maxAngle);
            return angle;
        }
    }
}