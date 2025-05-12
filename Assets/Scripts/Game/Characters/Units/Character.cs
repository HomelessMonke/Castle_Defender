using Game.Characters.States;
using UnityEngine;

namespace Game.Characters.Units
{
    public abstract class Character: MonoBehaviour
    {
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
}