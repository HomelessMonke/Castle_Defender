using Game.Characters.Movements;
namespace Game.Characters.States
{
    public class MoveState : IState
    {
        IMovement movement;
        
        public IMovement Movement => movement;

        
        
        public MoveState(IMovement movement)
        {
            this.movement = movement;  
        }
        
        public void Enter()
        {

        }
        
        public void Update()
        {
            
        }
        
        public void Exit()
        {

        }
    }
}