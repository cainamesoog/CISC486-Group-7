# CISC486-Group-7

## Game Title
**Witch Way**

---

## Core Gameplay
The 2 players will take on the role of witches who are trying to complete their potion-making. To do this, they must collect 5 magical ingredients found in a labyrinth to put into their cauldron. 
There will be knights patrolling the labyrinth, and they will chase the players when spotted. When a knight successfully catches up to a witch and attacks them, that witch is dead. 
However, the other player can come revive the witch. Each witch has an ability at their disposal, and both have flying brooms that need to be recharged over time (sprinting).

- The win condition is that the witches must collect the ingredients in sequential order and throw them into the cauldron until the potion is done, all while trying not to get caught/make contact with the knights.
- The loss condition is that both witches are dead.


## Game Type
The game type will be a **Maze Chase + Puzzle Adventure** game

This is because the players will perform the following:
- Traverse a maze to collect ingredients while avoiding knights (Maze Chase)
- Witches will need to use their abilities to solve puzzles and progress through blocked areas (Puzzle Adventure) 

---

## Player Setup
This will be a **2-player local coop**. 
There will be 2 witches with distinct abilities that the players can only choose from:
- **Fire Witch:** casts Fireball to burn plants and clear overgrown paths.
- **Wind Witch:** casts Zephyr to push objects and reveal hidden routes.
Both these abilities have cooldowns, so the witches must use them sparingly, either for puzzles or escape.

---

## AI Enemies (NPCs) – FSM & Decision-Making
The enemies in this game will be knights sent to eliminate the witches.
The knights' behaviours will act according to a Finite State Machine:
The **FSM** will look like this:
1. **Patrol:** walk back and forth over a specific path.
2. **Pursuit:** chase after a witch in the detection radius.  
3. **Attack:** strike a witch in the attack radius.  
4. **Stunned:** temporarily immobilized if hit by a witch’s ability.  
5. **Reset:** return to the original patrol path.  

The **decision-making** is as follows:
- Patrol → Pursuit: a witch ends up in the knight’s detection radius.
- Pursuit → Attack: a witch ends up in the knight’s striking radius.
- Attack → Pursuit: the witch leaves the striking radius.
- Pursuit → Reset: the chased witch loses the knight’s aggro.
- Pursuit → Stunned: the witch hits the knight with an ability while in detection radius.
- Attack → Stunned: the witch hits the knight with an ability while in striking radius.
- Stunned → Pursuit: the witch is still in detection radius when the stun duration ends.
- Stunned → Attack: the witch is still in striking radius when the stun duration ends.
- Stunned → Reset: the witch is no longer in the detection radius.
- Reset → Patrol: the knight returns to its original path to continue patrolling.
- Reset → Pursuit: the knight detects a witch while it’s trying to return to its original patrol.

---

## Scripted Event
The scripted events are tied to the witches’ progress with the potion-making. 
At specific thresholds, the knights will become harder, specifically:
- **Ingredients 1-2:** Slightly increase detection radius and movement speed.  
- **Ingredients 3-4:** Further increase detection radius and movement speed.  
- **Ingredient 5:** Knights abandon patrol and **relentlessly chase** the witch carrying the final ingredient.  
During the final chase, the knights can still be Stunned, but they will not enter the Reset state, creating a high-tension last push.  

---

## Puzzle Elements
- **Blocked Paths:** Arcane Witch must move objects to open routes.  
- **Rune Doors:** Require both witches to activate switches together.  
- **Guarded Corridors:** Fire Witch burns plants to clear paths.  

---

## Environment
The game will take place at the witches’ dungeon, which happens to be a convoluted maze.
In the middle of the maze is where the witches will initially spawn, including where the cauldron is located.
There will be areas of the map that have to be opened via the witches’ abilities, where Fireball will be used for burning down plants, and Zephyr will push objects away. 
The ingredient objectives will be spawned all around the map. 

---
## Basic Planning Factors

## Assets
**Visual Assets:** I will be using free models found online for witches, knights, the cauldron, walls, floors, torches, herbs, crystals, etc.
**Audio Assets:** I will be using free sounds also found online for footsteps, ambient dungeon noise, cauldron bubbling, item collection, etc. I may also include voice acting and royalty-free music.  

## Team Information
- **Group Number:** 7  
- **Name:** Jay Wu  
- **ID:** 20346992  

## Team Roles
All roles, including programming, level design, asset sourcing, and documentation, will be handled by Jay Wu.
