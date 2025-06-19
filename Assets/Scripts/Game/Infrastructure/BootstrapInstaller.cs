using Game.Currencies;
using Game.Grades;
using Game.Signals;
using Game.Signals.AllyArcher;
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
            BindWaveProgressCounter();
        }

        void BindSelf()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this);
        }
        
        void BindGradesList()
        {
            Container.Bind<AllGradesSequenceList>().FromInstance(gradesSequenceList);
        }
        
        void BindWaveProgressCounter()
        {
            Container.Bind<WaveProgressCounter>().AsSingle();
        }

        void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AllyArchersCountUpgradeSignal>();
            Container.DeclareSignal<AllyArchersDamageUpgradeSignal>();
            Container.DeclareSignal<CastleHealthUpgradeSignal>();
            Container.DeclareSignal<WaveFinishedSignal>();
            Container.DeclareSignal<LaunchWaveSignal>();
            Container.DeclareSignal<DespawnEnemySignal>();
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
            InitGameInitializer();
        }
        
        void InitGameInitializer()
        {
            var initializer = new GameInitializer();
            Container.Inject(initializer);
            initializer.Init();
        }

        void InjectToGradeSequences()
        {
            Container.Inject(gradesSequenceList);
            gradesSequenceList.Init();
        }
    }
}