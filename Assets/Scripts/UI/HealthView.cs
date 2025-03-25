using Game.Characters;
using UnityEngine;

namespace UI
{
    public class HealthView: MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer fillArea;

        public void Draw(HealthComponent health)
        {
            SetActive(true);
            fillArea.transform.localScale = new Vector3(health.Percentage, 1, 1);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}