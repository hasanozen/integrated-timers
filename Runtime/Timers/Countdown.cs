using UnityEngine;

namespace IntegratedTimer
{
    public class Countdown : Timer
    {
        public override bool IsFinished => CurrentTime <= 0;
        
        public Countdown(float value) : base(value) { }

        public override void Tick()
        {
            if (IsRunning && CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
            }

            if (IsRunning && CurrentTime <= 0)
            {
                Stop();
            }
        }
    }
}