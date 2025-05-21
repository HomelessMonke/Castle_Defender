using System.Collections.Generic;
using System.Linq;
using Game.Characters.Parameters;
using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using Game.Signals.AllyArcher;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public class AllyArchersSpawner : MonoBehaviour
    {
        [SerializeField]
        AllyArcher archerTemplate;

        [SerializeField]
        AllyArchersParameters parameters;
        
        [SerializeField]
        Transform startSpawnPoint;

        [SerializeField]
        Transform root;

        [SerializeField]
        ProjectileSpawner arrowForTowerSpawner;
        
        [SerializeField]
        LinesSpawnFormation formation;
        
        List<AllyArcher> archers = new ();

        SignalBus signalBus;

        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        
        public void Start()
        {
            signalBus.Subscribe<AllyArchersCountUpgradeSignal>(OnArchersCountIncreased);
            signalBus.Subscribe<AllyArchersDamageUpgradeSignal>(OnArchersDamageIncreased);
        }

        public void SpawnAllyArchers()
        {
            SpawnArchersAtPositions(GetSpawnPoints(parameters.ArchersCount));
        }

        void SpawnArchersAtPositions(IEnumerable<Vector2> positions)
        {
            foreach (var spawnPos in positions)
            {
                SpawnArcher(spawnPos);
            }
        }

        void SpawnArcher(Vector2 spawnPos)
        {
            var archer = Instantiate(archerTemplate, spawnPos, Quaternion.identity, root);
            archer.gameObject.SetActive(true);
            archer.Init(parameters, arrowForTowerSpawner);
            archers.Add(archer);
        }

        void UpdateArchersPositions(Vector2[] positions)
        {
            for (int i = 0; i < archers.Count; i++)
            {
                archers[i].transform.position = positions[i];
            }
        }

        Vector2[] GetSpawnPoints(int count)
        {
            formation.SetLinePositionsCounts(count, parameters.MaxInLine);
            return formation.GetSpawnPoints(startSpawnPoint);
        }

        void OnArchersCountIncreased(AllyArchersCountUpgradeSignal signal)
        {
            var positions = GetSpawnPoints(parameters.ArchersCount);
            UpdateArchersPositions(positions);
            var newArchersPositions = positions.TakeLast(signal.AddCount);
            SpawnArchersAtPositions(newArchersPositions);
        }
        
        void OnArchersDamageIncreased(AllyArchersDamageUpgradeSignal signal)
        {
            var damage = parameters.Damage;
            foreach (var archer in archers)
            {
                archer.SetAttackParameter(damage);
            }
        }
    }
}