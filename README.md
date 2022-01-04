# asteroids-scriptable-objects

This is my gradable assignment called `asteroids-scriptable-objects` for the course **Development tools in game projects** at [Futuregames](http://futuregames.se/). You control a spaceship that destroys asteroids, it's a classic spaceshooter game. 

***Coded by Tomas Wallin***

## What I did

In this course you had the options to select 3 different types of assignments:
1. **Scriptable Events Feature.**\
Implement a gameplay feature using one or more Scriptable Events.
2. **Asteroid Destroyer.**\
      Implement big asteroids splitting up into smaller pieces before being destroyed completely.
3. **Event Debugger.**\
      Create a debugger class that can toggle sending Debug.Logs from our events, telling us what event was fired and with what data.

*I did both assignment 2 (Asteroid Destroyer) and 3 (Event Debugger).*

### Assignment 2 - Asteroid Destroyer
Whenever a asteroid is being hit, it checks the size of the asteroid. If the asteroid is big enough it halves the hitted asteroids size, and instantiates another one in the same size as asteroid that got hit.

The files to check out:\
`Asteroid.cs`, `AsteroidDestroyer.cs` and `AsteroidSet.cs`\
\
The events to check out:\
`OnAsteroidHitEvent`, `OnAsteroidSplitEvent` and `OnAsteroidDestroyedEvent`
### Assignment 3 - Event Debugger
I created an event debugger (`EventDebugger.cs`) that saves all the fired events into a list, but only if the custom defined symbol `CUSTOM_LOG` is being set.\
In addition to the debugger, I created a custom window (`EventDebugConsole.cs`) that displays all the event-assets as checkboxes, also displays the logged events that has been fired.

#### Side note:
I added a `ScrollViewScope` for the list of logs, but I didn't have the time to figure out how to autoscroll with the list whenever a new log is being added like the Console in Unity. So I reversed the list and displays the last log first, but it's something I want to try to fix in the future.

The directory to check out:\
`Assets/_Game/Scripts/Core/Tools/EventDebugger`
### Other
Another thing I did was fixing the UI so it properly worked. Updating whenever health is being lost, when a asteroid is destroyed, when a laser is being fired, and a countdown timer. 
