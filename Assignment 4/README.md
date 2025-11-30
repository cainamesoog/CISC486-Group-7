# CISC486-Group-7

---

## Networking  
Multiplayer was implemented using **PurrNet** for session hosting and **ParallelSync** for creating and testing multiple player instances.  
 
--- 

## Setup/Run Instructions  
  
If the latest version of Purrnet and parallel are already installed, please skip to step 3.  
  
1. In Unity's package manager, please install the latest version of Purrnet directly or from installing the package from the following git url: https://github.com/PurrNet/PurrNet.git?path=/Assets/PurrNet#dev  
  
2. Download the ParallylSync unity package from the following git url and run the unitypackage with the current project open: https://github.com/VeriorPies/ParrelSync/releases/tag/1.5.2
  
3. At the top of the Unity editor, open the ParallelSync tab and choose Clones Manager. Create a new clone and then open in new editor when it's done cloning.  
   
4. Run the game on the server side / main project.  
  
5. Run the game on the client side / cloned project.  
  
6. Now you can see both the host and client players.  
 
--- 

## Player Controls  
- **Move:** WASD or Arrow Keys  
- **Sprint:** Hold Left Shift  
- **Jump:** Spacebar  
- **Spawn Enemy:** P  
- **Open Chat:** T  
- **Send Message:** Enter  
  
---  
  
## Demo Video  
[Gameplay Video (YouTube Unlisted)](https://youtu.be/Zq38bcO6zOA)  
  
---  
  
## Notes
- The changes were made in the Networking branch of the repository.
- Player and enemy are still represented by capsule colliders.   
- Enemy colour indicates state:  
  - Green = Patrol  
  - Red = Pursuit   
  - Black = Attack   
  - Blue = Reset  
- Gizmo colours:  
  - Green = Detection Radius   
  - Black = Attack Radius   
- Both players are grey capsules 
- Chat currently does not work as intended for multiplayer