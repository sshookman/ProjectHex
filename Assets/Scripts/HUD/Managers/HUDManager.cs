
/// <summary>
/// This script controls the text and images that display on the HUD
/// during the game. The HUD provides information to the player about
/// the selected tile and actor.
/// </summary>
public static class HUDManager : object {

    private static Actor actor;
    private static Tile tile;

    /// <summary>
    /// Setter for the Actor
    /// </summary>
    /// <param name="newActor">HUD Actor selected</param>
    public static void setActor(Actor newActor) {
        actor = newActor;
    }

    /// <summary>
    /// Getter for the Actor
    /// </summary>
    /// <returns>Actor - HUD selected</returns>
    public static Actor getActor() {
        return actor;
    }

    /// <summary>
    /// Setter for the Tile
    /// </summary>
    /// <param name="newTile">HUD Tile selected</param>
    public static void setTile(Tile newTile) {
        tile = newTile;
    }

    /// <summary>
    /// Getter for the Tile
    /// </summary>
    /// <returns>Tile - HUD selected</returns>
    public static Tile getTile() {
        return tile;
    }
}