using System.Collections.Generic;
using Game.Currencies;
using Game.Signals;
using UnityEngine;
using Utilities;
using Zenject;

namespace Game.UI.Currencies
{
    public class CurrenciesPanel : MonoBehaviour
    {
        [SerializeField]
        CurrencyView[] views;
        
        Dictionary<CurrencyType,CurrencyView> viewsDict = new ();

        CurrencyManager currencyManager;

        [Inject]
        void Construct(CurrencyManager currencyManager)
        {
            this.currencyManager = currencyManager;
        }

        public void Init()
        {
            currencyManager.OnCurrencyChanged += OnCurrencyChanged;
            foreach (var view in views)
            {
                viewsDict.Add(view.Type, view);
            }
        }

        public void DrawViews()
        {
            foreach (var type in viewsDict.Keys)
            {
                DrawViewByType(type);
            }
        }

        void DrawViewByType(CurrencyType type)
        {
            var value = currencyManager.GetAmount(type);
            var convertedValue = NumberFormatter.FormatNumber(value);
            viewsDict[type].Draw(convertedValue);
        }

        void OnCurrencyChanged(CurrencyType type, int value)
        {
            var convertedValue = NumberFormatter.FormatNumber(value);
            viewsDict[type].Draw(convertedValue);
        }
    }
}