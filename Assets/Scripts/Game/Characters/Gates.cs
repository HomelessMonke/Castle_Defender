using UI;
using UnityEngine;

namespace Game.Characters
{
    public class Gates : MonoBehaviour
    {
        [SerializeField]
        HealthComponent health;

        [SerializeField]
        DamageFlash damageFlash;
        
        [SerializeField]
        HealthView hpView;
        
        public void Init(int maxHealth)
        {
            health.Init(maxHealth);
            health.DamageTaken += ()=>
            {
                hpView.Draw(health);
                damageFlash.Flash();
            };
            health.Died += ()=> hpView.SetActive(false);
        }
    }
}