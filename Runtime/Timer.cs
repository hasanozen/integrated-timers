using System;
using UnityEngine;

namespace IntegratedTimer
{
    public abstract class Timer : IDisposable
    {
        private bool _disposed;
        
        protected float InitialTime;
        
        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; protected set; }
        
        public abstract bool IsFinished { get; }
        
        public Action OnStart = delegate { };
        public Action OnStop = delegate { };

        public float Progress => Mathf.Clamp(CurrentTime / InitialTime, 0, 1);

        #region Creational

        protected Timer(float initialTime)
        {
            InitialTime = initialTime;
        }
        
        ~Timer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) Manager.Unregister(this);

            _disposed = true;
        }

        #endregion

        #region Control Unit

        public void Start()
        {
            CurrentTime = InitialTime;

            if (!IsRunning)
            {
                IsRunning = true;
                Manager.Register(this);
                OnStart.Invoke();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                Manager.Unregister(this);
                OnStop.Invoke();
            }
        }
        
        public abstract void Tick();

        public void Resume()
        {
            IsRunning = true;
        }
        
        public void Pause()
        {
            IsRunning = false;
        }

        public virtual void Reset()
        {
            CurrentTime = InitialTime;
        }

        public virtual void Reset(float newTime) 
        {
            InitialTime = newTime;
            Reset();
        }

        #endregion
    }
}
