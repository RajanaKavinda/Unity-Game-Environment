# Unity Game Environment - Team CodeCrafters (Group No 13)

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
- **Enemies:** Once a barrier is destroyed, new enemies (Blue Slimes) are spawned, adding challenges as the player progresses.
- **Coins and Dash:** The player can collect coins and dash for faster movement.
- **Shop:** Collected coins can be used to buy more trees and bushes from the shop and add them to the inventory.
- **Inventory Management:** The player can drag and drop inventory items within the game environment as desired.

## Logics

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

This repository contains the game's source code, assets, and documentation necessary for understanding and contributing to the project.
