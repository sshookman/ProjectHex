using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script provides a conveneince wrapper for the Tiles used in pathfinding algorithms
/// </summary>
public class TileList {

    private Dictionary<Vector2, Tile> tiles;

    /// <summary>
    /// Default constructor that initializes the Dictionary to new
    /// </summary>
    public TileList() {
        tiles = new Dictionary<Vector2, Tile>();
    }

    /// <summary>
    /// Getter for the node Dictionary
    /// </summary>
    /// <returns>Dictionary - nodes</returns>
    public Dictionary<Vector2, Tile> GetTileDictionary() {
        return tiles;
    }

    /// <summary>
    /// Setter for the node Dictionary
    /// </summary>
    /// <param name="nodes">node Dictionary</param>
    public void SetTileDictionary(Dictionary<Vector2, Tile> nodes) {
        this.tiles = nodes;
    }

    /// <summary>
    /// Adds a new Tile to the NodeList
    /// </summary>
    /// <param name="node">Tile node</param>
    public void Add(Tile node) {
        Vector2 coord = new Vector2(node.GetX(), node.GetY());
        tiles.Add(coord, node);
    }

    /// <summary>
    /// Getter for a Tile node given it's Vector2 coordinates
    /// </summary>
    /// <param name="coord">Vector2 coordinates</param>
    /// <returns>Tile - node</returns>
    public Tile Get(Vector2 coord) {
        return tiles[coord];
    }

	/// <summary>
	/// Get the closest tile to the given coordinates, but not the 
	/// tile with the given coordinates
	/// </summary>
	/// <returns>Tile - closest</returns>
	/// <param name="x">The x coordinate</param>
	/// <param name="y">The y coordinate</param>
    public Tile GetClosest(int x, int y) {

        Vector2 target = new Vector2(x, y);
        Tile closestTile = null;
        float closestDistance = 0f;

        foreach (KeyValuePair<Vector2, Tile> pair in tiles) {
            Vector2 tileVector = new Vector2(pair.Value.GetX(), pair.Value.GetY());
			if ((!closestTile || Vector2.Distance(tileVector, target) < closestDistance) && !tileVector.Equals(target)) {
                closestTile = pair.Value;
                closestDistance = Vector2.Distance(tileVector, target);
            }
        }

        return closestTile;
    }

	/// <summary>
	/// Get a random Tile from the list
	/// </summary>
	/// <returns>Tile - random</returns>
	/// <param name="maxX">Max x position</param>
	/// <param name="maxY">Max y position</param>
    public Tile GetRandom(int maxX, int maxY) {
        Vector2 randomCoord;

        do {
            int y = Random.Range(0, maxX);
            int x = Random.Range(0, maxY);
            randomCoord = new Vector2(y, x);
        } while (!Contains(randomCoord));

        return tiles[randomCoord];
    }

    /// <summary>
    /// Removes a Tile node from the NodeList
    /// </summary>
    /// <param name="node">Hextile node</param>
    public void Remove(Tile node) {
        tiles.Remove(new Vector2(node.GetX(), node.GetY()));
    }

    /// <summary>
    /// Removes a Tile node from the NodeList given it's Vector2 coordinates
    /// </summary>
    /// <param name="coord">Vector2 coordinates</param>
    public void Remove(Vector2 coord) {
        tiles.Remove(coord);
    }

    /// <summary>
    /// Getter for the size of the NodeList
    /// </summary>
    /// <returns>int - count of nodes in the NodeList</returns>
    public int GetSize() {
        return tiles.Count;
    }

    /// <summary>
    /// Checks if a Tile exists in the NodeList given it's Vector2 coordinates
    /// </summary>
    /// <param name="coord">Vector2 coordinates</param>
    /// <returns>bool - exists</returns>
    public bool Contains(Vector2 coord) {
        return tiles.ContainsKey(coord);
    }

    /// <summary>
    /// Checks if a Tile exists in the NodeList
    /// </summary>
    /// <param name="node">Tile node</param>
    /// <returns>bool - exists</returns>
    public bool Contains(Tile node) {
        return tiles.ContainsValue(node);
    }

    /// <summary>
    /// Getter for the node in the NodeList with the lowest cost
    /// </summary>
    /// <returns>Tile - lowest cost node</returns>
    public Tile GetLowestCost() {

        Tile lowestNode = null;

        foreach (KeyValuePair<Vector2, Tile> pair in tiles) {
            if (!lowestNode || lowestNode.GetTotalCost() > pair.Value.GetTotalCost()) {
                lowestNode = pair.Value;
            }
        }

        return lowestNode;
    }

    /// <summary>
    /// Getter for the Tile nodes in the NodeList for iteration
    /// </summary>
    /// <returns>List - Tile nodes</returns>
    public List<Tile> GetTiles() {
        List<Tile> tiles = new List<Tile>();

        foreach (KeyValuePair<Vector2, Tile> pair in this.tiles) {
            tiles.Add(pair.Value);
        }

        return tiles;
    }
}
