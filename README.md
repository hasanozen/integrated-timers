# Integrated Timer System

The Integrated Timer System is a flexible and modular Unity-based timer management system. It supports various types of timers including countdowns, stopwatches, and tickers that can be easily integrated into Unity projects. The system is designed to be efficient, reusable, and easily extendable.

## Features

- Countdown Timer: Counts down from a set initial time to zero.
- Stopwatch: Counts upwards infinitely from zero.
- Ticker: Ticks a specified number of times per second.
- Modular Timer Management: The system allows for easy management and registration of timers.
- Player Loop Integration: Timers are updated via the Unity Player Loop to ensure accurate and reliable execution.

## Components

### 1. Timer (Abstract Base Class)
The Timer class serves as the base for all timers, providing common functionality such as starting, stopping, resetting, and tracking progress. It supports methods for ticking each frame and can be extended to create different types of timers.

Key Methods:
- Start(): Starts the timer.
- Stop(): Stops the timer.
- Tick(): Abstract method to be implemented by derived classes.
- Pause() / Resume(): Pauses and resumes the timer.
- Reset(): Resets the timer to the initial time.

### 2. Countdown Timer
A timer that counts down from an initial time to zero. Once the time reaches zero, the timer stops.

### 3. Stopwatch Timer
A timer that counts infinitely upwards, starting from zero.

### 4. Ticker Timer
A timer that ticks a specified number of times per second. Useful for regularly triggering events in the game loop.

### 5. Manager
The Manager class handles the registration and updating of all timers in the system. Timers are updated via the Unity Player Loop, ensuring they are updated at the correct intervals without needing a MonoBehaviour for each timer.

### 6. Bootstrapper
The Bootstrapper class integrates the Timer system into Unity's Player Loop. It inserts the Manager into the appropriate part of the update cycle and ensures that the system is properly initialized when the game starts.

## How It Works

### Initialization
When the Unity game loads, the Bootstrapper automatically integrates the timer system into Unity's PlayerLoop using the RuntimeInitializeOnLoadMethod attribute.

### Timer Lifecycle
Registration: Timers are registered with the Manager when started, and unregistered when stopped or disposed.
Update: The Manager.UpdateTimers() method is called every frame, updating all registered timers by calling their Tick() methods.
Clearing: Timers are cleared automatically when the game exits play mode (in the Unity Editor) or when manually disposed.

### Example Usage

```csharp
private readonly Countdown _countdown = new(5f);

private void Start()
{
    _countdown.OnStart += () => Debug.Log("Countdown started");
    _countdown.OnStop += () => Debug.Log("Countdown stopped");
            
    _countdown.Start();
            
    _countdown.Pause();
    _countdown.Resume();
            
    _countdown.Reset();
    _countdown.Reset(10f);
            
    _countdown.Stop();
}
        
private void Update()
{
    Debug.Log($"Current Time is {_countdown.CurrentTime}");
    Debug.Log($"Progress is {_countdown.Progress}");
}

private void OnDestroy()
{
    _countdown.Dispose();
}
```

## Setup & Usage
* Add the Timer System to Your Project
    * Download or clone the repository and import the code into your Unity project.

* Create a Timer
  * You can create a countdown, stopwatch, or ticker timer by inheriting from the Timer base class and implementing the Tick() method.
  Start and Manage Timers

* Use the Manager class to register and update timers, and ensure timers are properly disposed of when no longer needed.

## Contributing
If you'd like to contribute to the project, feel free to fork the repository and submit a pull request with your improvements.

## License
This project is open-source and available under the MIT License.

````
Let me know if you need anything else!
````

