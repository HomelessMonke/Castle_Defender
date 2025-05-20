using System;
using Game.Currencies;
using UnityEngine;

namespace Game.Upgrades
{
    [Serializable]
    public class FloatGrade
    {
        [SerializeField]
        float upgradeValue;

        [SerializeField]
        CurrencyItem currency;
        
        public float UpgradeValue => upgradeValue;
        public CurrencyItem Currency => currency;
    }

}