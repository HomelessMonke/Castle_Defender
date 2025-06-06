﻿using System.Collections.Generic;
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

        SignalBus signalBus;
        CurrencyManager currencyManager;

        [Inject]
        void Construct(SignalBus signalBus, CurrencyManager currencyManager)
        {
            this.signalBus = signalBus;
            this.currencyManager = currencyManager;
        }

        void Start()
        {
            signalBus.Subscribe<CurrencyChangedSignal>(OnCurrencyChanged);
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

        void OnCurrencyChanged(CurrencyChangedSignal signal)
        {
            DrawViewByType(signal.CurrencyType);
        }
    }
}