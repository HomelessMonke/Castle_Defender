using Game.Characters.Parameters;
using Game.Characters.Units;
using Game.Currencies;
using Game.Waves;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public class EnemyRangedCharacterSpawner: CharacterSpawner
    {
        [SerializeField]
        ObjectsPool<EnemyRangedCharacter> pool;
        
        [SerializeField]
        EnemyRangedParameters unitParameters;
        
        [SerializeField]
        ProjectileSpawner projectileSpawner;
        
        [Space(10)]
        [SerializeField]
        Transform spawnPointTransform;
        
        //TODO: перенести оффсеты в squadInfo или отдельынй класс, на который будет ссылать squadInfo
        [SerializeField]
        protected float unitsOffsetX;
        
        [SerializeField]
        protected float unitsOffsetY;
        
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
            int unitsCount = squadInfo.Count;
            ArmyFormation armyFormation = new ArmyFormation(spawnPointTransform.position, unitsOffsetX, unitsOffsetY, squadInfo.MaxUnitsInRow);
            var characters = new Character[unitsCount];
            for (int i = 0; i < unitsCount; i++)
            {
                var spawnPos = armyFormation.GetSpawnPoint(i);
                characters[i] = Spawn(spawnPos);
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
            pool.Despawn(unit);
            currencyService.Earn(CurrencyType.Soft, unitParameters.CoinReward);
            Debug.Log($"CoinReward = {unitParameters.CoinReward}");
            Debug.Log($"CurrencyType.Soft: {currencyService.GetAmount(CurrencyType.Soft)}");
        }
    }
}