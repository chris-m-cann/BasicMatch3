# Basic Match 3

A bejewled classic rip-off made in unity with a basic shapes in glowing neon aesthetic.

![In game screenshot](https://github.com/chris-m-cann/BasicMatch3/blob/master/readmeart/Capture.PNG)

This was my first "finished" game made over about 2 months on-and-off. I learned a lot in the course of making it and had fun doing it so in those ways it was a success. Yay!! Go me!! Having said that, there is still a lot missing and I took a whole bunch of wrong turns that I want to avoid on my next project. So I thought a postmortom/ retrospective might help future Chris make something faster and better next time (no pressure future Chris).

### Retrospective

The main goals of the project were:

1. Make a game that runs on mobile
3. Learn about creating modular code architecture when developing in unity
3. Actually finish the game



These are some of the main failing of the project:

1. Performance
2. Platforms
3. Infrastructure
4. Looks



These are the main takeaways from this project:

1. The perfect is the enemy of the done
4. Use third party assets for things you aren't trying to learn
5. Event based architectures help to decouple objects within your scene
8. Figure out your target platforms and early
9. Add testability into your design



### Failings



![doh](https://github.com/chris-m-cann/BasicMatch3/blob/master/readmeart/facepalm.jpg)



##### 1. Performance

I made this game with a mind to extensibility rather than performance. As such I do several things that I wouldn't do to the same extent again. These things include:

- extensive use of Linq : Though readable and very usable, Linq isn't a recommended thing to do unity. As it says in "9 ways to optimize your game development: Unity expert tips to help you ship" a best practice guide from Unity: "avoid using them in critical paths (i.e., regular updates), as they can generate a lot of managed allocations and be expensive in terms of CPU cost" 
- An excessive number of allocations: Objects are allocated willy nilly as and when needed to make the code fit the paradigms I put in place. Though this can keep the code cleaner (eg returning new List<Match>()) all over the place to avoid null checks), it creates a lot of garbage. To combat this in the future I will look to modifying exiting arrays rather than transforming them into new ones, techniques like object pooling and the cost of some of my favored features like lambdas.
- Not profiling as I go: As mentioned, performance was not a core goal of this project so I did not concern myself with profiling and optimizing my hot-paths. This is something I will do differently on the next project as without at least keeping an eye on performance as the game evolves I may be condemning myself to lengthy refactors when a core design ends up manifesting a visible issue.

##### 2. Platforms

My main goal was to have a game playable on mobile, however, I didn't think about where the game would end up, and now with the game complete the idea of making it work on the web to upload it to a site like Gamejolt or itch.io is a bit daunting. In future I will try and make sure it works on multiple platforms as I develop

##### 3. Infrastructure

The game has a main menu, a game scene and a couple of pop up menus. Thats about it. No daily challenges. No scoreboards. Not even a personal best for the player to strive for. Future projects should be built with this kind of thing in mind rather than leaving it as an afterthought once the game is more or less done. 

##### 4. Looks

Damn it Jim, I'm a programmer not an artist! I tried to keep the games aesthetic fairly simple. Just basic shapes and effects. But its still pretty damn fugly and not very sharp on some devices. For future games I need to improve upon this. Who want to play something boring looking?



#### Takeaways

![tasty learning](https://github.com/chris-m-cann/BasicMatch3/blob/master/readmeart/learnding.gif)



##### 1. The perfect is the enemy of the done

This game took far longer that it should have. This is as it was basically re-written 3 times as I tried to make it cleaner and cleaner. I started out with 90% of the gameplay in under a week but the code was hacked to pieces. Giant classes, direct dependencies, it was a mess. The following month was mostly cleaning it up and adding a couple of small features. This didn't leave much time for polish as originally it was supposed to be a 1 month game max. 

The main takeaways from this are to code a feature fully before moving on to the next. Some quick and dirty prototyping is to be expected but don't code yourself into a corner. After a rough version of a feature is proven go back and write it as a modular piece that fits as a whole. Also, polish as you go rather than all at the end. This will help make sure you don't write code that doesn't allow for this later as well as maintaining engagement on the project overall. More than that, you don't want to wait a month to figure out if your game is going to "feel" right.

##### 2. Use third party assets for things you aren't trying to learn

I used the following unity asset bundles:

- "Universal Sound FX" for the explosion sounds
- "Ultimate Game Music Collection" for the music
- "LightningBolt" for the lighning effects
- "JMO Assets" for the Square Bomb explosion
- "Text Mesh Pro" for UI

I am no artist. Making these things would have taken me weeks and been nowhere near as good. Programming and design are my focuses, leaning on amazing asset creators for the rest is the only way to get anything done.

##### 3. Event based architectures help to decouple objects within your scene

For this project I used ScriptableObjects to build an event based architecture with interchangeable components. This technique came from a couple of [unite](https://www.youtube.com/watch?v=raQ3iHhE_Kk) [talks](https://www.youtube.com/watch?v=6vmRwLYWNRo&t=905s) I saw and [this](https://unity3d.com/how-to/architect-with-scriptable-objects) supporting article. It was very helpful to get my head around how to write extensible decoupled code in unity while still having objects communicate. I do however think I took it a bit too far. Events are great for decoupling but not for sequencing. I will certainly use similar techniques in future project but need to add in more patterns such as mixins behaviors. ScriptableObjects where a bit of a breakthrough for me in terms of writing interchangeable components and injecting them into objects through the inspector. This helped me leverage some of my OO knowledge from other forms of programming, my struggle to do so had been frustrating me up until then. However, since Prefabs became more flexible people are saying they may be the better choice. I will investigate this next time around.

##### 4. Figure out your target platforms and early

This one is dead obvious. Figure out where it's going to go and make sure it works on that platform throughout the development. First rule of cross platform. Do better than me future Chris.

##### 5. Add testability into your design

This is something I added during one of my many refactors and it worked out great. Being able to build static custom starting grids to test out certain scenarios proved invaluable. Definitely endeavor to make sure any state can be recreated statically in the inspector. That way you can just build a test scene and find issues without having to play the game to get to that state. I could have gone further in this for this game and made it more data driven.



### Goals

Ultimately it's not a great game. Not even a good game. But it did achieve my goals in the sense that is complete in the technical sense and taught me a lot about code architecture in unity. There is a lot I could have done better and a lot to bear in mind for the next project. But thats a job for future Chris. Good luck!!