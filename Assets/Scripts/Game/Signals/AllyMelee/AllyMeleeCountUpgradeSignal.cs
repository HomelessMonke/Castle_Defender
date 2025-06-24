namespace Game.Signals.AllyMelee
{
    public struct AllyMeleeCountUpgradeSignal
    {
        public int AddCount { get; private set; }

        public AllyMeleeCountUpgradeSignal(int addCount)
        {
            AddCount = addCount;
        }
    }
}