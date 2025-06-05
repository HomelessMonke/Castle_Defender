using Game.Currencies;
using TMPro;
using UnityEngine;

namespace Game.UI.Currencies
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField]
        CurrencyType type;
        
        [SerializeField]
        TextMeshProUGUI text;

        public CurrencyType Type => type;

        public void Draw(int value)
        {
            text.text = value.ToString();
        }
    }
}