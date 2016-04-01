
/// <summary>
/// Enum for the different states of the TBS game element
/// </summary>
public enum GameState { LOADING, HUMAN_TURN, GHOUL_TURN, VICTORY, DEFEAT };

/// <summary>
/// Enum for the different states of the map
/// </summary>
public enum MapState { SELECTION, MOVEMENT, ATTACK, SKILL, LOCKED, GHOULS };

/// <summary>
/// Enum for the different states of a the tile
/// </summary>
public enum TileState {
    DEFAULT, HOVERED, SELECTED, //SELECTION
    REACHABLE, PATHWAY,         //MOVEMENT
    ATTACKABLE, TARGET,         //ATTACK
    LOCKED, DISABLED
};         //GENERAL

/// <summary>
/// Enum for the different types of terrain a tile can use
/// </summary>
public enum TileType { GRASS, TREES, DIRT, WATER };