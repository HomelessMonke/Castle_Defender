using Game.Characters.States;
using UI;
using UnityEngine;
using UnityEngine.AI;
using IState = Game.Characters.States.IState;

namespace Game.Characters
{

    public class SwordsMan : Character
    {
        [SerializeField]
        NavMeshAgent agent;

        [SerializeField]
        CharacterFieldOfView fieldOfView;
        
        [SerializeField]
        HealthView healthView;
        
        CharacterMoveState moveState;
        
        IState currentState;
        
        public void Init(Transform mainTarget, CharacterParameters parameters)
        {
            health.Init(parameters.Hp);
            health.DamageTaken += ()=> healthView.Draw(health);
            health.Died += ()=> healthView.SetActive(false);
            
            currentState = moveState = new CharacterMoveState(agent);
            moveState.SetTarget(mainTarget);
            moveState.Enter();
        }

        void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }
    }
}