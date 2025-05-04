using UI;
using UnityEngine;

namespace Game.Characters
{
    public class Gates : MonoBehaviour
    {
        [SerializeField]
        HealthComponent health;
        
        [SerializeField]
        HealthView hpView;
        
        public void Init(int maxHealth)
        {
            health.Init(maxHealth);
            health.DamageTaken += ()=>
            {
                hpView.Draw(health);
            };
            health.Died += ()=> hpView.SetActive(false);
        }
    }
}