using Game.Characters.Parameters;
using Game.Characters.Spawners.FormationSpawnParameters;
using Game.Characters.Units;
using Game.Currencies;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public class EnemyMeleeCharacterSpawner: CharacterSpawner
    {
        [SerializeField]
        ObjectsPool<MeleeCharacter> pool;
        
        [SerializeField]
        EnemyMeleeUnitParameters unitParameters;
        
        [Space(10)]
        [SerializeField]
        Transform spawnPointTransform;
        
        CurrencyService currencyService;
        
        [Inject]
        public void Constructor(CurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        public override void Init()
        {
            pool.Init();
        }
        
        public override Character[] Spawn(SquadInfo squadInfo)
        {
            var spawnPositions = squadInfo.GetSpawnPoints(spawnPointTransform.position);
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

        void OnDie(MeleeCharacter unit)
        {
            pool.Despawn(unit);
            currencyService.Earn(CurrencyType.Soft, unitParameters.CoinReward);
            Debug.Log($"CoinReward = {unitParameters.CoinReward}");
            Debug.Log($"CurrencyType.Soft: {currencyService.GetAmount(CurrencyType.Soft)}");
        }
    }
}