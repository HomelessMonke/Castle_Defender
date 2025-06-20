using System;
using UnityEngine;

namespace Game.UI.Popups.UpgradesPopupSpace
{
    [Serializable]
    public struct UpgradeViewAnimatorData
    {
        [SerializeField]
        float showDuration, hideDurationStep1, hideDurationStep2;
        
        [SerializeField]
        Vector2 beforeShowScale, foldScale, expandScale;
        
        public float ShowDuration => showDuration;
        public float HideDurationStep1 => hideDurationStep1;
        public float HideDurationStep2 => hideDurationStep2;
        
        public Vector2 FoldScale => foldScale;
        public Vector2 ExpandScale => expandScale;
        public Vector2 BeforeShowScale => beforeShowScale;
    }
}