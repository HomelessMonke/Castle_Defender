using UnityEngine.Events;

namespace Utilities
{
    public class CustomTimer
    {
        public float Duration { get; private set; }
        public float TimeLeft { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsFinished => TimeLeft <= 0f;

        public event UnityAction OnTimerEnd;

        public CustomTimer(float duration)
        {
            Duration = duration;
            TimeLeft = duration;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Reset()
        {
            TimeLeft = Duration;
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public void Tick(float deltaTime)
        {
            if (!IsRunning || IsFinished) 
                return;

            TimeLeft -= deltaTime;

            if (IsFinished)
            {
                TimeLeft = 0f;
                IsRunning = false;
                OnTimerEnd?.Invoke();
            }
        }
    }
}