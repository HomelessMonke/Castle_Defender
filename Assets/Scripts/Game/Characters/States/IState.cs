namespace Game.Characters.States
{
    public interface IState
    {
        bool CanSelfEnter { get; }
        void Enter();
        void Update();
        void Exit();
    }
}