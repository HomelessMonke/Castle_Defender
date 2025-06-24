using System;
using Utilities.Attributes;

namespace Game.UI.BaseUiScope
{
    [Serializable]
    [AsEnumSource]
    public static class UIElementIDList
    {
        public const string StartWaveButton = "StartWaveButton";
        public const string PlayerAbilities = "PlayerAbilities";
        public const string CoinsBar = "Coins";
        public const string HpBar = "HpBar";
        public const string UpgradesButton = "UpgradesButton";
        public const string SpeedChangeButton = "SpeedChangeButton";
        public const string PauseButton = "PauseButton";
    }
}