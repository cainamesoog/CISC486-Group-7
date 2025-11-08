# CISC486-Group-7

---

## FSM 
The NPC AI was implemented using a finite state machine. 
 
### States  
- **Patrol:** The knight moves between patrol points.  
  Patrol now follows a back and forth movement structure.  
  AI no longer glides over the patrol points like before.   
- **Pursuit:** When the witch enters the detection radius, the knight chases her.  
- **Attack:** When close enough, the knight stops and performs an attack.  
- **Reset:** If the witch escapes the detection radius, the knight returns to the next patrol point to continue patrolling.
 
### Transitions  
- **Patrol → Pursuit:** the witch enters the knight’s detection radius.   
- **Pursuit → Attack:** the witch enters striking range.   
- **Pursuit → Reset:** the witch leaves detection range and the knight loses aggro.   
- **Attack → Pursuit:** the witch leaves striking range.   
- **Reset → Patrol:** the knight reaches its last patrol point and resumes patrolling.   
- **Reset → Pursuit:** the knight detects the witch again during reset. 

--- 

## Pathfinding 
The NPC AI's movement was implemented with Unity’s built-in **NavMeshAgent** for pathfinding.  
The knight moves precisely between patrol points with no terrain clipping or sliding. 
 
--- 

## Player Controls  
- **Move:** WASD or Arrow Keys  
- **Sprint:** Hold Left Shift  
- **Jump:** Spacebar  

---

## Demo Video
[Gameplay Video (YouTube Unlisted)](...)

---

## Notes
- Player and enemy are still represented by capsule colliders.   
- Enemy colour indicates state:  
  - Green = Patrol  
  - Red = Pursuit   
  - Black = Attack   
  - Blue = Reset  
- Gizmo colours:  
  - Green = Detection Radius   
  - Black = Attack Radius   