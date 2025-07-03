using Game.Signals;
using UnityEngine;
using Zenject;

namespace Game.Waves
{
    public class WaveProgressCounter
    {
        int currentCount;
        SignalBus signalBus;

        public void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void SetCount(int count)
        {
            currentCount = count;
            Debug.Log($"SET currentCount: {currentCount}");
        }
        
        public void DecreaseCount()
        {
            currentCount--;
            Debug.Log($"currentCount: {currentCount}");
            if (currentCount == 0)
            {
                signalBus.Fire(new WaveFinishedSignal(true));
            }
        }
    }
}