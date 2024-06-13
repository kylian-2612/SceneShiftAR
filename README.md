# SceneShiftAR

## Overview

This Unity-based application allows users to select items from a library and place them in a scene. Once an item is selected, the library panel closes, and the item can be manipulated with movement, rotation, and scaling tools. The application features intuitive UI controls and transformation options to enhance the user experience.

## Features
- Asset Library: Browse and select from a variety of assets.
- Scene Placement: Instantly place selected items at a designated spawn point.
- Transformation Tools:
- Movement: Move objects along the X, Y, and Z axes.
- Rotation: Rotate objects around the X, Y, and Z axes.
- Scaling: Adjust the size of objects along the X, Y, and Z axes.
- Interactive Arrows: Visual arrows guide the user during transformations.
- Locking Mechanism: Lock specific axes to restrict movement, rotation, or scaling.
- Export Transformations: Save object transformations to a text file.

## Setup
### Prerequisites
- Unity 2020.3 LTS or newer
- Standard Unity Asset Store packages (e.g., UI Toolkit, TextMeshPro)

## Installation
### Clone the Repository:

1. Clone the repository using Git: 
   ```
   git clone https://github.com/kylian-2612/SceneShiftAR.git
   ```
2. Navigate to the project directory:
   ```
   cd AssetPlacementTool
   ```

### Open the Project in Unity:

Launch Unity Hub, and open the AssetPlacementTool project.

### Install Dependencies:

Ensure all necessary assets and packages are imported. Check the Packages folder and Assets directory for required assets.

## Configuration
1. Assign Prefabs:

In the Unity Editor, navigate to the `AssetLibrary` script component and assign:

- `assetButtonPrefab` with the button prefab.
- `moveArrowPrefab`, `rotateArrowPrefab`, `scaleArrowPrefab` with their respective arrow prefabs.
- `uiPanel` and `gridContainer` for the asset selection UI.

2. Set Spawn Point:

Assign the `spawnPoint` transform where new assets will be instantiated.

## Usage
### Selecting and Placing Assets
1. Open the asset library by clicking the "Open Library" button.
2. Browse and click on an asset to place it in the scene.
### Transforming Assets
1. Select an asset in the scene to activate transformation tools.
2. Toggle between Move, Rotate, and Scale modes using the UI buttons.
3. Use the arrows to manipulate the selected asset along the respective axes.
4. Lock/unlock axes as needed to constrain transformations.
### Exporting Transformations
1. Click the "Export Transformations" button to save the current state of all placed assets to a text file.
2. The text file will contain position, rotation, and scale data for each asset.

## Scripts Overview
- AssetButton.cs: Handles the creation and initialization of asset buttons in the library.
- AssetLibrary.cs: Manages asset loading, button creation, and asset selection.
- CubeMovement.cs: Handles movement logic with axis locking.
- CubeRotation.cs: Manages rotation with axis locking.
- CubeScaling.cs: Controls scaling with axis locking.
- SelectableItem.cs: Detects item selection and triggers UI updates.
- UIManager.cs: Manages UI interactions, mode toggling, and transformation exporting.

## Contribution
Feel free to fork the repository, make changes, and submit a pull request. For any issues or feature requests, please create an issue on GitHub.

## License
This project is licensed under the MIT License. See the LICENSE file for details.
