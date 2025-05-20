using System;
using UnityEngine;

namespace Game.Currencies
{
    [Serializable]
    public class CurrencyItem
    {
        [SerializeField]
        CurrencyType type;
        
        [SerializeField]
        int amount;

        public CurrencyType Type => type;
        public int Amount => amount;
        
        public CurrencyItem(CurrencyType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
    }
}