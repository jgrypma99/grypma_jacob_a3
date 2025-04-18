a simple clone of the classic game snake with an ai opponent in the Unity game engine. The game board is laid out in a grid with players able to change direction up down 
left and right with the arrow keys and movement in those directions being automatic. Players can hold down the shift key to dash and increase their movement speed. 
The opponent utilizes a Finite State Machine (FSM) for its behavior.

Each segment of the snakes will be represented by a square sprite for simplicity. 
Snakes will start out 3 segments (both the player and opponent) long and increase in length by one segment with each point collected. 
Each segment will follow the movement of the segment before it like in the classic version of the game.

Opponent snake AI behaviors:
Opponent will prioritize collecting points
When player approaches within 5 grid spaces of the opponent will switch priority to attempt to cut off the player and have them lose by colliding with the opponent
When players dash, the opponent snake will switch into a more defensive mode, attempting to avoid the player.

Player death by colliding with the opponent and opponent death by colliding with the player remain unimplemented, however this could easily be handled
by simply checking if the player head or the opponent head collide with the segments of either the player or the opponent respectively. The enemy snake does
however behave as if these features existed, attempting to cut off the player when they get too close, and fleeing when the player dashes near them.
