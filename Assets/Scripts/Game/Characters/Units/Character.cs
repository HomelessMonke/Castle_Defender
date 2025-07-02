using Game.Characters.States;
using UnityEngine;

namespace Game.Characters.Units
{
    public abstract class Character: MonoBehaviour
    {
        protected const int sortingLayerMedium = 100;
        protected const int sortingLayerMultiplier = 10;
        
        protected IState currentState;
        
        protected void SetState(IState newState)
        {
            if (newState.Equals(currentState) && !newState.CanSelfEnter)
                return;
            
            currentState?.Exit();
            newState.Enter();
            currentState = newState;
        }
    }

    public interface IDamageable
    {
        public string Id { get; }
        public bool IsAlive { get; }
        public Transform Transform { get; }
        public Health HealthComponent { get; }
    }
}