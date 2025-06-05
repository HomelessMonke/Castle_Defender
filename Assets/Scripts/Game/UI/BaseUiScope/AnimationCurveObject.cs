using UnityEngine;

namespace Game.UI.BaseUiScope
{
    [CreateAssetMenu(menuName = "Animation/AnimationCurve", fileName = "AnimationCurve")]
    public class AnimationCurveObject : ScriptableObject
    {
        [SerializeField]
        AnimationCurve curve;
        
        public AnimationCurve Curve => curve;
    }
}