using Game.Currencies;
using Game.Grades;
using Game.Signals.AllyArcher;
using Game.Signals.Castle;
using Game.Upgrades;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
    public class BootstrapInstaller: MonoInstaller, IInitializable
    {
        [SerializeField]
        GradesSequenceList gradesSequenceList;
        
        public override void InstallBindings()
        {
            BindSelf();
            BindSignals();
            BindCurrencyService();
        }

        void BindSelf()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this);
        }

        void BindCurrencyService()
        {
            var currencyService = new CurrencyManager();
            currencyService.Init();
            Container.Bind<CurrencyManager>().FromInstance(currencyService);
        }
        
        void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AllyArchersCountUpgradeSignal>();
            Container.DeclareSignal<AllyArchersDamageUpgradeSignal>();
            Container.DeclareSignal<CastleHealthUpgradeSignal>();
        }

        public void Initialize()
        {
            InjectToGradeSequences();
        }
        
        void InjectToGradeSequences()
        {
            Container.Inject(gradesSequenceList);
            gradesSequenceList.Init();
        }
    }
}