using System;
using System.Collections.Generic;

namespace Game.Currencies
{
    public class CurrencyManager
    {
        const string SaveKeyPrefix = "Currency_";

        Dictionary<CurrencyType, int> currencies = new();

        public Action<CurrencyType, int> OnCurrencyChanged;

        public void Init()
        {
            foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            {
                int savedValue = ES3.KeyExists(SaveKeyPrefix + type)
                    ? ES3.Load<int>(SaveKeyPrefix + type)
                    : 0;

                currencies[type] = savedValue;
            }
        }

        public int GetAmount(CurrencyType type)
        {
            return currencies.GetValueOrDefault(type, 0);
        }

        public void Earn(CurrencyType type, int amount)
        {
            currencies[type] += amount;
            OnCurrencyChanged?.Invoke(type, currencies[type]);
        }

        public bool Spend(CurrencyItem currencyItem)
        {
            return Spend(currencyItem.Type, currencyItem.Amount);
        }

        bool Spend(CurrencyType type, int amount)
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

        public void SaveCurrencies()
        {
            foreach (var pair in currencies)
            {
                ES3.Save(SaveKeyPrefix + pair.Key, pair.Value);
            }
        }
        
        public void ResetCurrencies()
        {
            foreach (var currency in currencies)
            {
                var type = currency.Key;
                currencies[type] = 0;
                ES3.DeleteKey(SaveKeyPrefix + type);
            }
        }
    }
}