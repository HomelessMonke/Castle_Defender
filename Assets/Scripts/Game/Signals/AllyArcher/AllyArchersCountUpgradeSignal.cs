namespace Game.Signals.AllyArcher
{
    public struct AllyArchersCountUpgradeSignal
    {
        public int AddCount { get; private set; }

        public AllyArchersCountUpgradeSignal(int addCount)
        {
            AddCount = addCount;
        }
    }

}