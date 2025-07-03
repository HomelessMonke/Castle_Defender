using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using Game.Currencies;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public abstract class EnemyCharacterSpawner: MonoBehaviour
    {
        [SerializeField]
        protected Transform spawnPointTransform;
        
        [SerializeField]
        EnemyType enemyType;
        
        public EnemyType EnemyType => enemyType;

        public abstract void Init (CurrencyManager currencyService, SignalBus signalBus);

        public abstract Character[] Spawn(SquadInfo squadInfo);

        public abstract void DespawnAllUnits();

        public abstract void SetAllUnitsIdleState();
    }
}