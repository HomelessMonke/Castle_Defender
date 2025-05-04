namespace Game.Characters.States
{
    public interface IState
    {
        bool CanSelfEnter { get; }
        void Enter();
        void Update();
        void Exit();
    }

    public class IdleState: IState
    {
        public bool CanSelfEnter => false;

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