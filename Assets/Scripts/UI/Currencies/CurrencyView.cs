using Game.Currencies;
using TMPro;
using UnityEngine;

namespace UI.Currencies
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField]
        CurrencyType type;
        
        [SerializeField]
        TextMeshProUGUI text;

        public CurrencyType Type => type;

        public void Draw(string value)
        {
            text.text = value;
        }
    }
}