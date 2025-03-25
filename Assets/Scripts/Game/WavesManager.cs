using System.Collections;
using Game.Characters;
using Game.Characters.Spawners;
using Game.Waves;
using UI;
using UnityEngine;

namespace Game
{
    public class WavesManager: MonoBehaviour
    {
        [SerializeField]
        CharactersSpawner charactersSpawner;
        
        [SerializeField]
        WavesList wavesList;
        
        public void LaunchWaves()
        {
            StartCoroutine(LaunchWavesCoroutine());
        }

        IEnumerator LaunchWavesCoroutine()
        {
            foreach (var wave in wavesList.Waves)
            {
                yield return SpawnWave(wave);
                yield return new WaitForSeconds(wave.NextWaveDelay);
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
    }
}