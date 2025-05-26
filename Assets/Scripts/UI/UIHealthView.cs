using Game.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHealthView: MonoBehaviour
    {
        [SerializeField]
        Image filler;

        [SerializeField]
        TextMeshProUGUI currentHpText;
        
        [SerializeField]
        TextMeshProUGUI maxHpText;

        public void Draw(Health health)
        {
            Draw(health.CurrentHp, health.Percentage);
            maxHpText.text = $"/ {(int)health.MaxHp}";
        }

        public void Draw(float currentHp, float percentage)
        {
            currentHpText.text = Mathf.CeilToInt(currentHp).ToString();
            filler.fillAmount = percentage;
        }
    }
}