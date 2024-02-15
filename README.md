# Max Pålsson Performance aware shooter
### Hardware use
Game is 2d and uses simple shapes to lower inpact on the GPU. It also uses scheduling and parallel scheduling to sort tasks and reduce lag.
 
 
### Data Orientation
Everything is handled in DOTS. Asteroids and bullets are instanciated and controlled inside of ISystems and IJobs. The playermovement is also
scheduled and executed inside of ISystems. Like stated earlier, it uses the dots built in scheduling system to sort out the order of ISystems and IjobEntities.
 
### Optimization through profiler
The programming process has been dragging on for a quite some times with a large amount of reworks on the code due to errors. To this, I used the profiler to see if jobs have been running and how much they've been affecting the computer.
It, in combination with the systems tab, have been very useful when jobs have repetadly not been running.

When it comes to optimization of the code, there too have I used the profiler to notice increaese in processing demand. One example of this was how bullets were being handled. Originally, the bullets didn't disappear after they had been fired, which caused 
huge spike in the profiler while fireing. That made me introduce a "death timer" for the bullets so they would dissapear after a few seconds. The bullets held priority over the asteroids because the bullets hade additional scripts that were connected to them
compared to the asteroids. It was still giving of big performance hits and also made it so that the more entities there were in the scene, the shorter the range of the bullets were. 
I noticed that in the systems tab, the amount of jobs for shooting kept increasing with the amount of asteroids on screen due to the bullets movement, it's hit registation and it's death timers were all handled in the same ISystem.

I decided to move the handleing of the death timers to the script that were in control of inputs and instantiating of bullets bacause it only used a single job at a time while working. 
But when I finally got it working through, it was still giving of big spikes in the profiler. So I introduced parallel scheduling to the timers on the bullets, which drasticlly decreased the strain on the CPU.
