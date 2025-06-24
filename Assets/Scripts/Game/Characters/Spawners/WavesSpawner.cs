using System.Collections;
using Game.Characters.Spawners.Formations;
using Game.Signals;
using Game.Waves;
using UnityEngine;
using Zenject;

namespace Game.Characters.Spawners
{
    public class WavesSpawner: MonoBehaviour
    {
        [SerializeField]
        EnemySpawnersList charactersSpawner;
        
        [SerializeField]
        WavesList wavesList;

        SignalBus signalBus;
        
        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void LaunchNextWave()
        {
            StartCoroutine(LaunchWave());
        }

        IEnumerator LaunchWave()
        {
            var wave = wavesList.NextWave;
            signalBus.Fire(new LaunchWaveSignal(wave.CharactersCount));

            if(wave == null)
                yield break;
            
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