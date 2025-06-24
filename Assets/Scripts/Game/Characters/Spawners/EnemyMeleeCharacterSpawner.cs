using Game.Characters.Parameters;
using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using Game.Currencies;
using Game.Signals;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public class EnemyMeleeCharacterSpawner: CharacterSpawner
    {
        [SerializeField]
        ObjectsPool<EnemyMeleeCharacter> pool;
        
        [SerializeField]
        EnemyMeleeParameters unitParameters;
        
        [Space(10)]
        [SerializeField]
        Transform spawnPointTransform;
        
        [SerializeField]
        LootBubbleSpawner bubbleSpawner;

        SignalBus signalBus;
        CurrencyManager currencyService;
        
        [Inject]
        public void Constructor(CurrencyManager currencyService, SignalBus signalBus)
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
            unit.Init(unitParameters);
            unit.Died+= ()=> OnDie(unit);
            return unit;
        }

        void OnDie(EnemyMeleeCharacter unit)
        {
            bubbleSpawner.Spawn(unit.transform.position, unitParameters.CoinReward);
            pool.Despawn(unit);
            currencyService.Earn(CurrencyType.Soft, unitParameters.CoinReward);
            signalBus.Fire<DespawnEnemySignal>();
        }
    }
}