namespace Game.Signals
{
    public struct LaunchWaveSignal
    {
        public int CharactersCount { get; }
        
        public LaunchWaveSignal(int charactersCount)
        {
            CharactersCount = charactersCount;
        }
    }
}