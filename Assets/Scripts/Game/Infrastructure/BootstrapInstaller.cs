using Game.Currencies;
using Game.Grades;
using Game.Signals;
using Game.Signals.AllyArcher;
using Game.Signals.AllyMelee;
using Game.Signals.Castle;
using Game.Waves;
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
        
        void BindGradesList()
        {
            Container.Bind<AllGradesSequenceList>().FromInstance(gradesSequenceList);
        }

        void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AllyArchersDamageUpgradeSignal>();
            Container.DeclareSignal<AllyArchersCountUpgradeSignal>();
            Container.DeclareSignal<AllyMeleeDamageUpgradeSignal>();
            Container.DeclareSignal<AllyMeleeHealthUpgradeSignal>();
            Container.DeclareSignal<AllyMeleeCountUpgradeSignal>();
            Container.DeclareSignal<CastleHealthUpgradeSignal>();
            Container.DeclareSignal<DespawnEnemySignal>();
            Container.DeclareSignal<WaveFinishedSignal>();
            Container.DeclareSignal<WaveLaunchedSignal>();
            Container.DeclareSignal<ResetGameBoard>();
        }

        void BindCurrencyService()
        {
            var currencyManager = new CurrencyManager();
            currencyManager.Init();
            Container.Bind<CurrencyManager>().FromInstance(currencyManager);
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