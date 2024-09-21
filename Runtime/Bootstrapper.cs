using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace IntegratedTimer
{
    internal static class Bootstrapper
    {
        private static PlayerLoopSystem _loopSystem;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            var currentLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!InsertManager<Update>(ref currentLoop, 0))
            {
                Debug.LogWarning("Failed to insert TimerManager into PlayerLoopSystem");
                return;
            }
            
            PlayerLoop.SetPlayerLoop(currentLoop);

#if UNITY_EDITOR
            
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;

            static void OnPlayModeState(PlayModeStateChange state)
            {
                if (state != PlayModeStateChange.ExitingPlayMode) return;
                
                var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
                RemoveManager<Update>(ref currentPlayerLoop);
                PlayerLoop.SetPlayerLoop(currentPlayerLoop);

                Manager.Clear();
            }
            
#endif
        }

        private static void RemoveManager<T>(ref PlayerLoopSystem loop)
        {
            LoopUtils.RemoveSystem<T>(ref loop, in _loopSystem);
        }

        private static bool InsertManager<T>(ref PlayerLoopSystem loop, int index)
        {
            _loopSystem = new PlayerLoopSystem
            {
                type = typeof(Manager),
                updateDelegate = Manager.UpdateTimers,
                subSystemList = null
            };
            
            return LoopUtils.InsertSystem<T>(ref loop, in _loopSystem, index);
        }
    }
}