using Game.Currencies;
using Game.Grades;
using Game.Signals;
using Game.Signals.AllyArcher;
using Game.Signals.Castle;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
    public class BootstrapInstaller: MonoInstaller, IInitializable
    {
        [SerializeField]
        AllGradesSequenceList gradesSequenceList;
        
        public override void InstallBindings()
        {
            BindSelf();
            BindSignals();
            BindGradesList();
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
            Container.DeclareSignal<CurrencyChangedSignal>();
        }

        void BindGradesList()
        {
            Container.Bind<AllGradesSequenceList>().FromInstance(gradesSequenceList);
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