using Game.Characters.States;

namespace Game.Characters
{
    public interface ICharacter
    {
        IState State { get; }
    }

}