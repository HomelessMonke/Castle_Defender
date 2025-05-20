namespace Game.Signals.AllyArcher
{
    public struct AllyArchersCountSignal
    {
        public int AddCount { get; private set; }

        public AllyArchersCountSignal(int addCount)
        {
            AddCount = addCount;
        }
    }
}