# Cookieman

![Unity](https://img.shields.io/badge/Unity-6000+-black?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![GitHub Pages](https://img.shields.io/badge/Play%20Online-4B8BBE?style=for-the-badge&logo=github&logoColor=white)

<img width="512" height="128" alt="Logo" align="middle" src="https://github.com/user-attachments/assets/c377c256-c4ee-4f6e-98a5-1753b651fd26" />

## Pacman like game made on Unity


## [Play Web](https://cyanv98.github.io/Cookieman/)

## How to Play
- **Arrow Keys** / **WASD** – Move Cookieman through the maze  
- **Collect all cookies** to complete the level  
- **Avoid ghosts** — if caught, you lose a life!  
- **Super cookies** : Temporarily turn the tables and eat ghosts!
  
## Key Features:
- Scriptables Objects for configurations
-  **[FSM](Assets/_Project/Scripts/FSM)** - state machine is used to determine the behavior of ghosts.
   - States:
     1) Scatter
     2) Chase
     3) Eaten
     4) Frightened
    - Transitions - define conditions for transition between states
- **[Grid](Assets/_Project/Scripts/Grid)** - the game is designed as a grid. Movement is also arranged by moving between cells.
- **[Primitive AI navigation](Assets/_Project/Scripts/Monsters/AINavigation.cs)** - the algorithm of the original game with searching for the closest cell to the target was copied.

<img width="50%" alt="Screenshot" src="https://github.com/user-attachments/assets/f25cfc99-3308-44b2-9b35-61db99bd3d84" />

## Future Features
- Implement full game lifecycle: start, pause, game over, restart
- Integrate **Zenject** for dependency injection and cleaner architecture
- Add **UniRX** for reactive programming and event handling
- Add sound effects and music
- Support multiple levels
- Mobile controls (touch input)

