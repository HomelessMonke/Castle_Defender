namespace Game.Characters.States
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}