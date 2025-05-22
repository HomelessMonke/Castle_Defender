using System;
using Game.Currencies;
using UnityEngine;

namespace Game.Upgrades
{
    [Serializable]
    public class Grade<T>
    {
        [SerializeField]
        T value;

        [SerializeField]
        CurrencyItem currency;
    
        public T Value => value;
        public CurrencyItem Currency => currency;
    }
}