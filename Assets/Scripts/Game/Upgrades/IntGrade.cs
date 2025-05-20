using System;
using Game.Currencies;
using UnityEngine;

namespace Game.Upgrades
{
    [Serializable]
    public class IntGrade
    {
        [SerializeField]
        int value;

        [SerializeField]
        CurrencyItem currency;
        
        public int Value => value;
        public CurrencyItem Currency => currency;
    }
}