using Game.Characters.Parameters;
using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using Game.Currencies;
using Game.Signals;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public class EnemyRangedCharacterSpawner: EnemyCharacterSpawner
    {
        [SerializeField]
        ObjectsPool<EnemyRangedCharacter> pool;
        
        [SerializeField]
        EnemyRangedParameters unitParameters;
        
        [SerializeField]
        ProjectileSpawner projectileSpawner;
        
        [SerializeField]
        LootBubbleSpawner bubbleSpawner;
        
        [Space(10)]
        [SerializeField]
        Transform spawnPointTransform;

        SignalBus signalBus;
        CurrencyManager currencyService;
        
        [Inject]
        public void Construct(CurrencyManager currencyService, SignalBus signalBus)
        {
            this.currencyService = currencyService;
            this.signalBus = signalBus;
        }

        public override void Init()
        {
            pool.Init();
        }
        
        public override Character[] Spawn(SquadInfo squadInfo)
        {
            var spawnPositions = squadInfo.GetSpawnPoints(spawnPointTransform);
            var count = spawnPositions.Length;
            var characters = new Character[count];
            for (int i = 0; i < count; i++)
            {
                characters[i] = Spawn(spawnPositions[i]);
            }
            return characters;
        }
        
        Character Spawn(Vector2 spawnPosition)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPosition;
            unit.Init(unitParameters, projectileSpawner);
            unit.Died+= ()=> OnDie(unit);
            return unit;
        }

        void OnDie(EnemyRangedCharacter unit)
        {
            bubbleSpawner.Spawn(unit.transform.position, unitParameters.CoinReward);
            pool.Despawn(unit);
            currencyService.Earn(CurrencyType.Soft, unitParameters.CoinReward);
            signalBus.Fire<DespawnEnemySignal>();
        }
    }
}