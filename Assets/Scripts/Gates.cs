using UnitComponents;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public Health HealthComponent{get; private set;}
    
    void Init(int maxHealth)
    {
        HealthComponent = new Health(maxHealth);        
    }
}
