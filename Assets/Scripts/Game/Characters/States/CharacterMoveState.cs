using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.States
{
    public class CharacterMoveState : IState
    {
        NavMeshAgent agent;
        Transform target;
        
        public CharacterMoveState(NavMeshAgent agent)
        {
            this.agent = agent;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void Enter()
        {
            //Включить проигрывание анимации
        }
        
        public void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
        
        public void Exit()
        {
            agent.ResetPath();
        }
    }
}