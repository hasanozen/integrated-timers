using System;
using UnityEngine;

namespace IntegratedTimer
{
    public class Ticker : Timer
    {
        private float _timeThreshold;
        
        public int Frequency { get; private set; }
        public Action OnTick = delegate { };
        
        public override bool IsFinished => !IsRunning;

        public Ticker(int ticks) : base(0)
        {
            CalculateThreshold(ticks);
        }

        public override void Tick()
        {
            if (IsRunning && CurrentTime >= _timeThreshold)
            {
                CurrentTime -= _timeThreshold;
                OnTick.Invoke();
            }

            if (IsRunning && CurrentTime < _timeThreshold)
            {
                CurrentTime += Time.deltaTime;
            }
        }

        public override void Reset()
        {
            CurrentTime = 0;
        }

        public void Reset(int ticks)
        {
            CalculateThreshold(ticks);
            Reset();
        }

        private void CalculateThreshold(int ticks)
        {
            Frequency = ticks;
            _timeThreshold = 1f / Frequency;
        }
    }
}