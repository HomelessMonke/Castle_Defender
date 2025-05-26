using UnityEngine;

namespace Game.Characters
{
    public class CharacterHealthView: MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer fillArea;

        public void Draw(Health health)
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