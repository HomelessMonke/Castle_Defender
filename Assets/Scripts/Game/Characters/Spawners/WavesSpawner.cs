using System.Collections;
using Game.Characters;
using Game.Characters.Spawners;
using Game.Characters.Spawners.FormationSpawnParameters;
using Game.Currencies;
using Game.Waves;
using UnityEngine;
using Utilities.Attributes;
using Zenject;

namespace Game.Characters.Spawners
{
    public class WavesSpawner: MonoBehaviour
    {
        [SerializeField]
        CharacterSpawnerList charactersSpawner;
        
        [SerializeField]
        WavesList wavesList;

        public void LaunchWaves()
        {
            StartCoroutine(LaunchWavesCoroutine());
        }

        IEnumerator LaunchWavesCoroutine()
        {
            var waves = wavesList.Waves;
            var timeBetweenWaves = wavesList.TimeBetweenWaves;
            foreach (var wave in waves)
            {
                yield return SpawnWave(wave);
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        IEnumerator SpawnWave(Wave wave)
        {
            foreach (var squad in wave.Squads)
            {
                yield return SpawnSquad(squad);
            }
        }

        IEnumerator SpawnSquad(SquadInfo squadInfo)
        {
            yield return new WaitForSeconds(squadInfo.SpawnDelay);
            charactersSpawner.Spawn(squadInfo);
        }
        
        [Button(runtimeOnly: true)]
        public void SpawnFirstWave()
        {
            StartCoroutine(SpawnWave(wavesList.Waves[0]));
        }
    }
}