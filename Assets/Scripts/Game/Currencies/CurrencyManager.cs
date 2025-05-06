using System.Collections.Generic;
using UnityEngine.Events;

namespace Game.Currencies
{
    public class CurrencyManager
    {
        public UnityEvent<CurrencyType, int> OnCurrencyChanged = new();

        Dictionary<CurrencyType, int> currencies = new();

        const string SaveKeyPrefix = "Currency_";

        void InitializeCurrencies()
        {
            foreach (CurrencyType type in System.Enum.GetValues(typeof(CurrencyType)))
            {
                int savedValue = ES3.KeyExists(SaveKeyPrefix + type)
                    ? ES3.Load<int>(SaveKeyPrefix + type)
                    : 0;

                currencies[type] = savedValue;
            }
        }

        public int GetAmount(CurrencyType type)
        {
            return currencies.TryGetValue(type, out var amount) ? amount : 0;
        }

        public void Add(CurrencyType type, int amount)
        {
            currencies[type] += amount;
            OnCurrencyChanged?.Invoke(type, currencies[type]);
        }

        public bool Spend(CurrencyType type, int amount)
        {
            if (currencies[type] >= amount)
            {
                currencies[type] -= amount;
                OnCurrencyChanged?.Invoke(type, currencies[type]);
                return true;
            }
            return false;
        }

        public void Set(CurrencyType type, int amount)
        {
            currencies[type] = amount;
            OnCurrencyChanged?.Invoke(type, currencies[type]);
        }

        public void SaveAll()
        {
            foreach (var pair in currencies)
            {
                ES3.Save(SaveKeyPrefix + pair.Key, pair.Value);
            }
        }
    }
}