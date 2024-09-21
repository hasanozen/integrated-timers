using System.Collections.Generic;

namespace IntegratedTimer
{
    public static class Manager
    {
        private static readonly List<Timer> Timers = new();
        private static readonly List<Timer> Sweep = new();

        public static void Register(Timer timer)
        {
            Timers.Add(timer);
        }
        
        public static void Unregister(Timer timer)
        {
            Timers.Remove(timer);
        }

        public static void UpdateTimers()
        {
            if (Timers.Count == 0) return;

            Sweep.RefreshWith(Timers);

            foreach (var timer in Sweep)
            {
                timer.Tick();
            }
        }

        public static void Clear()
        {
            Sweep.RefreshWith(Timers);

            foreach (var timer in Sweep)
            {
                timer.Dispose();
            }

            Timers.Clear();
            Sweep.Clear();
        }
    }
}