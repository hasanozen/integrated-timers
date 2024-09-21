using UnityEngine;

namespace IntegratedTimer
{
    public class Stopwatch : Timer
    {
        public override bool IsFinished => false;
        
        public Stopwatch() : base(0) { }

        public override void Tick()
        {
            if (IsRunning)
            {
                CurrentTime += Time.deltaTime;
            }
        }
    }
}