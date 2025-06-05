using System;
using Utilities.Attributes;

namespace Game.UI.BaseUiScope
{
    [Serializable]
    [AsEnumSource]
    public static class UIElementIDList
    {
        public const string PlayerAbilities = "Player_Abilities";
        public const string CoinsBar = "Coins";
        public const string HpBar = "HpBar";
    }
}