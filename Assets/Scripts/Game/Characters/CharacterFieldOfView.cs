using UnityEngine;

namespace Game.Characters
{
    public class CharacterFieldOfView : MonoBehaviour
    {
        [SerializeField]
        Collider2D collider2D;

        void OnTriggerStay2D(Collider2D other)
        {
            if(other.tag == "Player")
                Debug.Log($"OnTriggerStay2D {other.name}");
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Player")
                Debug.Log($"OnTriggerEnter2D {other.name}");
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            if(other.tag == "Player")
                Debug.Log($"OnTriggerExit2D {other.name}");
        }
    }
}