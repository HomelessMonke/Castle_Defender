using System.Collections;
using Game.Characters.Spawners;
using Game.Characters.Spawners.Formations;
using UnityEngine;

namespace Game.Waves
{
    public class WavesSpawner: MonoBehaviour
    {
        [SerializeField]
        EnemySpawnersList spawnersList;

        public void LaunchWave(Wave wave)
        {
            StartCoroutine(LaunchWaveCoroutine(wave));
        }

        IEnumerator LaunchWaveCoroutine(Wave wave)
        {
            if(wave == null)
                yield break;
            
            foreach (var squad in wave.Squads)
            {
                yield return new WaitForSeconds(squad.SpawnDelay);
                SpawnSquad(squad);
            }
        }

        void SpawnSquad(SquadInfo squadInfo)
        {
            var spawner = spawnersList.GetSpawnerByType(squadInfo.Type);
            spawner.Spawn(squadInfo);
        }
    }
}