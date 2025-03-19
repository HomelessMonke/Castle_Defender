using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.Movements
{
    public class WalkMovement: IMovement
    {
        NavMeshAgent agent;
        
        //TODO: если закладывать разные передвижения, ходьба и бег, то наверно стоит сделать MovementState, куда будут передаваться IMovement.
        public WalkMovement(NavMeshAgent agent)
        {
            this.agent = agent;
        }
        
        public void MoveToTarget(Vector3 position)
        {
            agent.SetDestination(position);
        }
    }
}