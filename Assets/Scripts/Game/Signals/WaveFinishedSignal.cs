namespace Game.Signals
{
    public struct WaveFinishedSignal
    {
        public bool IsVictory { get; }

        public WaveFinishedSignal(bool isVictory)
        {
            IsVictory = isVictory;            
        }
    }
}