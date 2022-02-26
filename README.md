# Udemy3DPlatformer
A platformer made with Unity Engine, based of a Udemy course.

This project is using assets and code from [James Doyles'](https://twitter.com/gamesplusjames) Udemy course titled [Unity 3D Platformer](https://www.udemy.com/course/unity3dplatformer/).Though I feel it is a very good first tutorial to learn the basics of Unity, thanks to the excellent pacing that almost brings one feature every fifteen minutes, I also regret that the code is not very well kept, in terms of organisation.

So I'm using the logic pretty much as is presented in the videos. But am also trying to improve on the code shown in said videos by refactoring, keeping hard coded values to a minimum, sometimes setting up the possibility to render things optional (like the knockback on damage) or customisable (like the amount of damage from obstacle and enemies using the DealDamage script), extracting methods instead of putting all the logic in the Update() method, and keeping the scripts tidy with regions that are consistent in every script.

The objective would be to have something pleasant to go through, but also customisable by simply adding scripts that would not intertwine with what is already present. Bringing (modestly) some good practices to this otherwise super fun project.

At some point, I'd also like to implement unit testing to the scripts, to prevent regression (especially in the movement system), and using Unity built in option for testing, in general.
