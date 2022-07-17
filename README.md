***__Grid__***
Levels are created with .csv files. Each grid file represents an individual tile.
Grids need a minimum size of 12x12. The playable area however can be smaller, the outer part of the grid needs to be set to NONE to maintain the minimum size.

The first row of the .csv file contains information about the level.
The required variables are as followed:
- title: string
- description: string
- gamemode: 
  - Reach the goal: 0
  - Survive rounds: 1
- gamemode settings

Each gamemode has its own settings, these settings are written like this:
- Reach the goal:
Example: 6-8-10
  - Rating system: 3 Stars - 2 Stars - 1 Star
- Survive rounds:
Example: 10-10-10
  - Rating system: 3 Stars - 2 Stars - 1 Star

***__Tiles__***
If a tile should be invisible, the tag has to be set to NONE as mentioned earlier.

The data tag of a tile should be based on this: ``FloortypeVersionnumber:Object_setting1-setting2-etc``
As an example, here is a tag of a standard wood tile with the starting position pointing west: ``W1:PLAYER_1-W``

As seen in this example, Tiles have two main properties: 

- The visual setting, which sets the texture of the tile.
The Visual Setting has a unique tag representing the texture of the tile block. 
For example, the standard wood tile is W1 (which stands for Wood - variant 1)

- The Object setting, which sets the object on the tile.
An object could be the player starting position, the goal, decoration or any other feature added to the game.
Object tiles have extra settings added to them. Keep in mind that each object type can have a different set of settings.

__***Tile Options***__
Tile Visual:

Tile Object:
- PLAYER
Starting position of player.
Example: PLAYER_1-N
Settings:
- Player Model:
  - Kid Justin: 1
  - Guitarist Djoest: 2
  - Hitman Karel: 3
- Direction:
  - North: N
  - West: W
  - South: S
  - East: E

- WALL
Wall Object, Movement is blocked.
Example: WALL_1-N
Settings:
- Wall Model:
  - Simple with support: 1
  - Simple corner with support: 2
  - Simple no support: 3
  - Simple corner no support: 4
- Direction:
  - North: N
  - West: W
  - South: S
  - East: E

- DOOR
Openable Door, Movement is blocked if door is closed.
Example: DOOR_1-N-O
Settings:
- Door Model:
  - Simple: 1
- Direction:
  - North: N
  - West: W
  - South: S
  - East: E
- State:
  - Open: O
  - Closed: C
- Linked Item:
  - Link: 1...10 (Note: Should be the same as connection)

- KEY
Key Object, Picked up when dice landed correctly.
Example: KEY_1-1
Settings:
- Key Model:
  - Standard key: 1
- Linked Item:
  - Link: 1...10 (Note: Should be the same as connection)

- GOAL
The Finish, Activated when dice landed correctly.
Example: GOAL_1
Settings:
- Goal Model:
  - Standard goal: 1