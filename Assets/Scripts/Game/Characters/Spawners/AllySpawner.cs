using System.Collections.Generic;
using Game.Characters.Spawners.Formations;
using Game.Characters.Units;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public abstract class AllySpawner<T> : MonoBehaviour where T : Character
    {
        [SerializeField]
        protected ObjectsPool<T> pool;
        
        [Space(10)]
        [SerializeField]
        protected Transform spawnPoint;
        
        [SerializeField]
        protected LinesSpawnFormation formation;
        
        protected List<T> units = new ();
        protected SignalBus signalBus;
        
        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public abstract void Init();
        public abstract void SpawnAllUnits();
        protected abstract void SpawnUnit(Vector2 spawnPos);
        
        protected void SpawnUnitsAtPositions(IEnumerable<Vector2> positions)
        {
            foreach (var spawnPos in positions)
            {
                SpawnUnit(spawnPos);
            }
        }
        
        protected void SetPositionsForUnits(Vector2[] positions)
        {
            for (int i = 0; i < units.Count; i++)
            {
                units[i].transform.position = positions[i];
            }
        }

        protected Vector2[] GetSpawnPoints(int count, int maxInLine)
        {
            var charactersInLineArr = GetCharactersLineArr(count, maxInLine);
            return formation.GetSpawnPoints(charactersInLineArr, spawnPoint);
        }
        
        int[] GetCharactersLineArr(int currentCount, int maxInLine, bool needReverse = false)
        {
            List<int> lineCounts = new();
            while (currentCount / maxInLine >= 1)
            {
                lineCounts.Add(maxInLine);
                currentCount-=maxInLine;
            }
            
            if(currentCount>0)
                lineCounts.Add(currentCount);

            if (needReverse && lineCounts.Count > 1)
                lineCounts.Reverse();
                
            return lineCounts.ToArray();
        }
    }
}