using Game.Currencies;
using Zenject;

namespace Game.Infrastructure
{
    public class BootstrapInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCurrencyService();
        }
        void BindCurrencyService()
        {
            var currencyService = new CurrencyService();
            currencyService.Init();
            Container.Bind<CurrencyService>().FromInstance(currencyService);
        }
    }
}