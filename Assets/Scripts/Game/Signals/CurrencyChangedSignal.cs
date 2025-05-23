using Game.Currencies;

namespace Game.Signals
{
    public struct CurrencyChangedSignal
    {
        CurrencyType type;
        
        public CurrencyType CurrencyType => type;
        
        public CurrencyChangedSignal(CurrencyType type)
        {
            this.type = type;
        }
    }
}