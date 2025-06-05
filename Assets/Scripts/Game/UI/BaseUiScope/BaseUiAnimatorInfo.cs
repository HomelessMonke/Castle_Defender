using System;
using UnityEngine;

namespace Game.UI.BaseUiScope
{
    public class BaseUiAnimatorInfo: ScriptableObject
    {
        [SerializeField]
        Vector2 offsetVector;
        
        [SerializeField]
        float duration;
        
        public Vector2 OffsetVector => offsetVector;
        public float Duration => duration;
    }
}