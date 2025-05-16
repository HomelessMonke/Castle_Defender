using System.Collections.Generic;
using Game.Characters.Parameters;
using Game.Characters.Spawners;
using Game.Characters.Units;
using UnityEngine;

namespace Game.Characters
{
    public class AllyArchersSpawner : MonoBehaviour
    {
        [SerializeField]
        AllyArcher archerTemplate;
        
        [SerializeField]
        int maxArchers;

        [SerializeField]
        int maxInRow;

        [SerializeField]
        float xOffset;
        
        [SerializeField]
        float yOffset;
        
        [SerializeField]
        Transform startSpawnPoint;

        [SerializeField]
        Transform root;
        
        [SerializeField]
        AllyArchersParameters parameters;
        
        [SerializeField]
        ProjectileSpawner arrowForTowerSpawner;
        
        List<AllyArcher> allyArchers = new ();
        
        public void SpawnAllyArchers()
        {
            for (int i = 0; i < maxArchers; i++)
            {
                var archer = SpawnArcher(i);
                archer.Init(parameters, arrowForTowerSpawner);
                allyArchers.Add(archer);
            }
        }

        AllyArcher SpawnArcher(int index)
        {
            var spawnPosition = GetSpawnPosition(index);
            var archer = Instantiate(archerTemplate, spawnPosition, Quaternion.identity, root);
            archer.gameObject.SetActive(true);
            return archer;
        }

        Vector2 GetSpawnPosition(int index)
        {
            var xOffset = startSpawnPoint.right * this.xOffset * (index%maxInRow);
            var yOffset = startSpawnPoint.up * this.yOffset * (index/maxInRow);
            return startSpawnPoint.position + xOffset - yOffset;
        }
    }
}