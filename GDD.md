# DevCon 3 Game Design Document
## Team 2

## Our Game Idea
A pong, [Insert theme] style game with simulated physics where the ball will be influenced by forces from the player’s paddles to the ground and ceiling. (this is a temporary placement for the idea that you can change)

It's the same as arcade pong, but instead of the ball being in “space” (no gravity), instead it will fall and hit the ground.
Project Statement/Project Goals
Implement a physics simulated twist on the original pong arcade game, influenced by metrics and forces. The project will blend the basis of pong with a dynamic physics system that intends to bring an enjoyable and engaging gameplay experience. 

## Our goals would be to:
Implement a physics based system that works with our pong game
Creating a theme that complements the game 
Making the game replayable, possibly with a scoring system (possible stretch goal) and from the unpredictability with physics being involved

## Design Rationale 
We believe a physics based “Pong” like game will enhance realism and add a layer of immersion within it. This is due to bounces, momentum, and speed adjustments that mimic real world behavior. Having an unpredictable version of Pong prevents repetition and allows players to adapt at a higher level of difficulty, which potentially creates an engaging experience with strong replayability.   


## Used Assets 


## Things we would need/Stretch goals
Need:
- Paddle controls (for two player)
- Ball physics / mechanic
- Scoring system (for final submission)

Stretch Goals:  
- Potential physic powerups
- Audio (background music, sound effects, etc.)
- Sprite Designs?

## Mechanic 
Our main mechanic is to score points with the ball using physics. This is a unique remake of pong whereas instead of a constant static ball, it's a object with a mass and center of gravity. In this way it adds a unique challenge and an interesting gameplay.

## Feedback and Implementation
Feedback we recieved 
- To make sure that the ball has a consistant horizontal velocity
- Or add an upward force whenever the ball touches one of the paddles, due to the ball having gravity, after a while it would start rolling across the ground
- The other piece of feedback was to implement a function that starts the game either after some sort of countdown or when one of the two players hit a button first to start

How we implemented it
- For the velocity, we kept most of the gravitational forces and vectors to bounce off of surfaces, but implemented a vector force that would force the ball upward at an angle, whether it's the left or right paddle
- Tried to implement a countdown function that would start the game but had difficulties with it just not working at all.

