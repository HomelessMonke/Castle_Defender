using Game.Characters.States;
using UnityEngine;

namespace Game.Characters
{

    public abstract class Character: MonoBehaviour
    {
        protected IState currentState;
        
        protected void ChangeState(IState newState)
        {
            currentState?.Exit();
            newState.Enter();
            currentState = newState;
        }
    }
}