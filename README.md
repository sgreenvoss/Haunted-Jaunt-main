# Haunted-Jaunt-main
 My haunted jaunt in which I make an invisiblizer. 

I worked alone for this project (so please be kind even though the dot product usage is stupid lol). 

**Dot Product**:
The dot product is used by the Observers to check if the player
is roughly in their view. If they are, the eye hovering over that observer
will appear to warn the player that they might get caught. This is easiest
to test by walking in front of a gargoyle without walking directly in its 
flashlight. An eye should appear over his head but the game-over screen 
should not appear.

**Lerping**:
I use linear interpolation to create a visual timer for the player. When 
invisiblized, I linearly interpolate the scale of the eye hovering above 
the player's head from all the way closed to all the way open. This way,
the player can relatively intuitively see the amount of invisibility they
have left. In the future I would properly mask over the pupil to create
a better eyeball illusion, but for now simple spritework will do.

**Particle Effect**:
I modified the shower particle system to place onto the invisiblizer in 
order to make it more magical looking. The invisiblizer itself is a trigger.
I may have forgotten to make a particle effect with trigger(s) but the 
particle system itself exists on a game object with a trigger, so I hope
that counts for something.

**Sound Effect**:
I added a sparkly sound effect that plays when the player steps onto the 
invisiblizer. Unfortunately there is some stupidity in how that is handled;
I could not figure out how to properly deal with John Lemon having two 
AudioSource components, so I decided to delegate the sound-playing to 
the eye floating above his head. So on trigger enter, when the trigger is
an invisiblizer, John calls eye.playAudio(). This is stupidity that I would
hope to factor out, but for now I am leaving it as-is in order to submit 
the project on time. 
