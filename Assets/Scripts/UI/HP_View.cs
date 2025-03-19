using UnityEngine;
using Utilities;

namespace UI
{
    public class HealthView: MonoBehaviour
    {
        [SerializeField]
        SlicedFilledImage fillArea;
        
        public void Draw(int curHP, int maxHP)
        {
            gameObject.SetActive(true);
            var fillAmount = (float)curHP / maxHP;
            fillArea.fillAmount = fillAmount;
        }
    }
}