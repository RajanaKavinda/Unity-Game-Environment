# Energy Quest Game - Team CodeCrafters (Group No 13)

![image](https://github.com/RajanaKavinda/Unity-Game-Environment/assets/91953272/18e1ac4f-c9cc-481c-b27f-3306a56449a6)

# Game Description and Behavior

## Overview
This game is a 2D environment where the player navigates through various lands with barriers that require certain achievements, referred to as quiz marks, to pass. The player's ability to pass through these barriers is determined by their quiz performance.

## Mechanics
- **Quiz Marks:** The player's progress is measured in quiz marks. For example:
  - A player with 70 quiz marks can pass through barriers.
  - Each barrier requires 10 marks to pass.
  - Hence, a player with 70 marks can pass through 70/10 = 7 barriers.
  - Every player starts with a default land, and with the additional lands accessible through their quiz marks, the total initial accessible land for the player is the default land + 7 = 8 lands.

- **In-Game Currency (Gems):** If a player does not have enough quiz marks, they can use in-game currency (gems) to unlock the barriers.

## Gems Calculation Method

### Daily Energy Consumption
Gems are awarded based on the player's daily energy consumption, which is fetched from APIs provided by the instructors. The calculation method is as follows:

1. **Fetch Daily Energy Consumption:**
   - For each day between the last played date and the current date, the player's daily energy consumption is fetched.

2. **Gem Calculation:**
   - Gems are calculated only for 50 coin value trees.
   - The number of trees planted in the game directly affects the number of gems collected.
   - If the daily energy consumption is:
     - Less than or equal to 3 units, the player earns 3 gems per tree planted.
     - Greater than 3 but less than or equal to 6 units, the player earns 2 gems per tree planted.
     - Greater than 6 but less than or equal to 9 units, the player earns 1 gem per tree planted.
     - Greater than 10 units, the player does not earn gems for the particular date.
       
## Additional Features
- **Enemies:** Once a barrier is destroyed, new enemies (Blue Slimes) are spawned, adding challenges as the player progresses. If player touches an enemy his coin count will be reduced. 
- **Coins and Dash:** The player can collect coins and dash for faster movement.
- **Shop:** Collected coins can be used to buy more trees and bushes from the shop and add them to the inventory.
- **Inventory Management:** The player can drag and drop inventory items within the game environment as desired.

## Real-Time Energy Consumption Indication

To get an understanding of the real-time energy consumption of the user, game lighting and trees are used as indicators. When current energy consumption is high, the game environment becomes darker and the trees start to die. When current energy consumption is low, the game environment becomes brighter and the trees start to fill with fruits and flowers.

### The Logic
1. **EnergyStatusController:**
   - Obtains current energy consumption periodically using HTTP GET requests.
   - Calculates the average energy consumption for 10 seconds over time.
   - Updates the state of energy-dependent entities like trees and lighting.

2. **TreeController:**
   - Animates tree growth and decay based on energy levels.
   - Receives updates from the EnergyStatusController.

3. **LightingController:**
   - Controls the color and intensity of the environment lighting.
   - Adjusts based on the average energy consumption received from the EnergyStatusController.
  
## State Persistence

   - The game state, including barrier destruction, total coins, gems,player position, inventory state and placed item state is saved using `PlayerPrefs`.
   - This ensures the player's progress is maintained across sessions.

## Player Controlling Keys
  Movements:
  
 - W: Move up
 - S: Move down
 - A: Move left
 - D: Move right
  
  Dashing:
  
 - Space: Player dashing


## How to Run This Game in Your Browser in Localhost

1. Clone this repository [MCQ-Application](https://github.com/Ishana-Dewmini/MCQ-Application) into your local repository.
2. Run `MCQ-Application/Backend` (Springboot appilication), `MCQ-Application/quiz-frontend`(React Vite application) on your local machine.
3. Clone this repository into your local repository.
4. Navigate to `Unity-Game-Environment -> WebGL Builds` folder.
5. Right click on the window and click `Open in terminal` (Command prompt / Windows Powershell will be opened).
6. Type the following command:
   ```
   py -m http.server
   ```
   (Install Python if you haven't)
7. Go to the browser and type the following URL: [http://localhost:8000/](http://localhost:8000/)

Follow these steps to enjoy playing the game on your own machine without the need for any additional setup or installations.

# Software Design Patterns used for main scripts in the Game

1. **Barrier**:
   - **Observer Pattern**: The `Barrier` class likely notifies the `BarrierCoinSpawner` when it is destroyed using an event (`OnBarrierDestroyed`).

2. **CoinManager**:
   - **Strategy Pattern**: Provides different strategies for modifying the coin count using methods like `DecreaseCoins`, `IncreaseCoins`, and `SetCoins`.

3. **CoinSpawner**:
   - **Factory Pattern**: Dynamically creates coin objects at runtime using predefined spawn points.
   - **Iterator Pattern**: Uses a coroutine (`SpawnCoinsRoutine`) to spawn coins at intervals.

4. **BarrierCoinSpawner**:
   - **Observer Pattern**: Subscribes to the `Barrier.OnBarrierDestroyed` event to spawn coins when a barrier is destroyed.
   - **Factory Pattern**: Similar to `CoinSpawner`, it dynamically creates coin objects.

5. **DataFetcher**:
   - **Singleton Pattern**: Ensures there is only one instance of `DataFetcher` and provides a global point of access to it.
   - **Iterator Pattern**: Uses a coroutine (`FetchDataCoroutine`) to handle data fetching step-by-step.
   - **Data Transfer Object (DTO)**: Uses classes (`PowerConsumptionResponse`, `DailyPowerConsumptionView`) to transfer data between the API and the game.

6. **EnemySpawner**:
   - **Factory Pattern**: Creates enemy objects dynamically at runtime using predefined spawn points.
   - **Iterator Pattern**: Uses a coroutine to manage the timing of enemy spawns.

7. **GemsDisplay**:
   - **Observer Pattern**: Likely observes changes in gem count to update the display accordingly.

8. **GemsManager**:
   - **Singleton Pattern**: Ensures a single instance of `GemsManager` for managing gems across the game.
   - **Observer Pattern**: Updates the display or other game elements when the gem count changes.

9. **PlayerController**:
   - **Singleton Pattern**: Ensures a single instance of `PlayerController` to manage the player state.
   - **Command Pattern**: Encapsulates player actions like movement and state changes as methods.

10. **SaveManager**:
    - **Singleton Pattern**: Ensures a single instance of `SaveManager` for managing save/load operations across scenes.
    - **Command Pattern**: Methods like `SavePlayerState`, `LoadPlayerState`, `SaveInventoryState`, and `LoadInventoryState` encapsulate different save/load operations.
    - **Iterator Pattern**: Uses loops to save/load placed items state.

11. **PauseWithPopup**:
    - **Singleton Pattern**: Ensures a single instance of `PauseWithPopup` to manage the pause state across the game.
    - **Command Pattern**: Encapsulates the pause and resume functionality as methods.

12. **TreeController**:
   - **Observer Pattern**: Implements the `IObserver` interface to receive updates from the `EnergyStatusController`. It updates the tree's animations based on the energy consumption and fruits or flowers count.
   - **State Pattern**: Manages different states (growth, partial death, complete death) through animations.
   - **Iterator Pattern**: Uses a coroutine (`UpdateCoroutine`) to handle updates step-by-step.

13. **LightingController**:
   - **Observer Pattern**: Implements the `IObserver` interface to receive updates from the `EnergyStatusController`. It adjusts the lighting based on energy consumption.
   - **Strategy Pattern**: Uses different strategies (`LowConsumptionLighting`, `MediumConsumptionLighting`, `HighConsumptionLighting`, `VeryHighConsumptionLighting`) to update the lighting condition based on the energy consumption level.

14. **EnergyStatusController**:
   - **Singleton Pattern**: Ensures a single instance of `EnergyStatusController` to manage energy status and notify observers.
   - **Observer Pattern**: Manages a list of observers (e.g., `TreeController`, `LightingController`) and notifies them when the energy consumption data is updated.
   - **Iterator Pattern**: Uses a coroutine (`UpdatePowerConsumption`) to periodically update the energy consumption data.

15. **HttpRequest**:
   - **Command Pattern**: Encapsulates different HTTP request operations (`get`, `post`, `put`, `delete`) as methods and handles their execution.
   - **Iterator Pattern**: Uses a coroutine (`SendHttpRequest`) to handle the asynchronous HTTP requests step-by-step.

By applying these design principles, the scripts are more modular, maintainable, and scalable, contributing to a well-structured and robust game architecture.

# Leaderboard 

![image](https://github.com/RajanaKavinda/Unity-Game-Environment/assets/91953272/cbe17647-925c-4ac4-b310-c1a41093bb39)

The leaderboard in this implementation displays a ranked list of players based on their scores. Each player's entry includes their rank, username, score, land count, gem count, and coin count. The current player's entry is highlighted to differentiate it from other players. The current player's entry is highlighted in red, while other players' entries are highlighted in blue.

## How Players Are Ranked

Players are ranked based on their scores, which are calculated using a custom scoring system. The scoring system considers the number of gems, lands, and coins each player has collected and applies specific weights to each. Current player's gems, lands are coin count is taken from the game itself.
Since we can not take energy consumption data & game performances for other players gems, lands and coins are randomly assigned for them. These randomly generated values are then used to calculate the score for each player as follows.

### Scoring Method

The scoring system is based on the following function:

```csharp
public static int CalculateScore(int gemCount, int landCount, int coinCount)
{
    return (gemCount * 5) + (landCount * 100) + (coinCount * 1);
}
```

- **Gems**: Each gem is worth 5 points.
- **Lands**: Each land is worth 100 points.
- **Coins**: Each coin is worth 1 point.

Hence, by collecting more gems, coins, and lands, a player can move to a higher position on the leaderboard.

## Main Steps in the Implementation

1. **Fetching User Data**:
   - A coroutine `FetchUserList` is used to fetch all players data from given backend server.
   - The fetched players data is parsed and stored in a list of `PlayerData`.

2. **Adding Current Player Data**:
   - The current player's data is fetched from the game environment and added to the `players` list.

3. **Updating the Leaderboard**:
   - Players are sorted by their scores in descending order.
   - Existing leaderboard entries are cleared.
   - New entries are created and populated with player data.
   - The current player's entry is highlighted in red, while other players' entries are highlighted in blue.

## Applied Design Patterns

### 1. **Singleton Pattern**
The `LeaderboardManager` class is designed to be a singleton, ensuring that only one instance of the leaderboard manager exists at any time. 

### 2. **MVC (Model-View-Controller) Pattern**
The implementation follows the MVC pattern:
- **Model**: `PlayerData` and the fetched user data.
- **View**: The `LeaderboardEntry` prefab and UI elements for displaying the leaderboard.
- **Controller**: `LeaderboardManager`, which handles data fetching, sorting, and updating the leaderboard UI.

### 3. **Factory Method Pattern**
The instantiation of leaderboard entries uses a factory-like approach by creating instances of the `leaderboardEntryPrefab` dynamically and populating them with data.

### 4. **Observer Pattern**
The `SyncHeight` class can be seen as an implementation of the observer pattern, where it observes the height of a target `RectTransform` and adjusts its own height accordingly.


This repository contains the game's source code, assets, and documentation necessary for understanding and contributing to the project.

