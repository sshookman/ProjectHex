using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script contains common functions that are used for performing
/// calculations for a HexMap
/// </summary>
public static class MapUtil {

	/// <summary>
	/// Checks to see if the two given Vector2 parameters are neighbors on the map
	/// </summary>
	/// <returns><c>true</c> if is neighbor the specified vectorA vectorB; otherwise, <c>false</c>.</returns>
	/// <param name="vectorA">Vector2</param>
	/// <param name="vectorB">Vector2</param>
	public static bool IsNeighbor(Vector2 vectorA, Vector2 vectorB) {
		return GetNeighbors((int)vectorA.x, (int)vectorA.y).Contains(vectorB);
	}

    /// <summary>
    /// This function takes in an x, y coordinate pair on a HexMap and
    /// constructs a list of neighbors for the given node.
    /// </summary>
    /// <param name="x">x coordinate of the primary node</param>
    /// <param name="y">y coordinate of the primary node</param>
    /// <returns>List of Vector2 neighbors</returns>
    public static List<Vector2> GetNeighbors(int x, int y) {

        List<Vector2> neighbors = new List<Vector2>();

        int offset = 1;
        if (x % 2 == 0) {
            offset *= -1;
        }

        neighbors.Add(new Vector2(x, y + 1));
        neighbors.Add(new Vector2(x, y - 1));
        neighbors.Add(new Vector2(x + 1, y));
        neighbors.Add(new Vector2(x - 1, y));
        neighbors.Add(new Vector2(x + 1, y + offset));
        neighbors.Add(new Vector2(x - 1, y + offset));

        return neighbors;
    }

    /// <summary>
    /// Constructs a TileList of the Tiles in the map that can be reached from the selected
    /// Tile with a given range.
    /// </summary>
    /// <param name="tiles">All of the tiles in the map</param>
    /// <param name="selected">The selected tile (origin of search)</param>
    /// <param name="mapWidth">Width of the map</param>
    /// <param name="mapHeight">Height of the map</param>
    /// <param name="range">The search distance maximum range</param>
    /// <param name="ignoreBlockers">Igore blocking objects such as other Actors</param>
    /// <returns>TileList - The reachable Tiles</returns>
    public static TileList BuildReachableTiles(GameObject[,] tiles, Vector2 selected, int mapWidth, int mapHeight, int range, bool ignoreBlockers) {

        TileList openTiles = new TileList();
        TileList closedTiles = new TileList();

        Tile selectedTile = tiles[(int)selected.x, (int)selected.y].GetComponent<Tile>();
        openTiles.Add(selectedTile);

        while (openTiles.GetSize() > 0) {

            Tile currentNode = openTiles.GetLowestCost();
            closedTiles.Add(currentNode);
            openTiles.Remove(currentNode);

            List<Vector2> neighbors = MapUtil.GetNeighbors(currentNode.GetX(), currentNode.GetY());

            neighbors.ForEach(delegate (Vector2 neighbor) {

                if (neighbor.x >= 0 && neighbor.x < mapWidth && neighbor.y >= 0 && neighbor.y < mapHeight &&
                        (ignoreBlockers || !tiles[(int)neighbor.x, (int)neighbor.y].GetComponent<Tile>().IsBlocked())) {
                    if (!closedTiles.Contains(neighbor)) {

                        if (openTiles.Contains(neighbor)) {
                            Tile neighborNode = openTiles.Get(neighbor);

                            if (neighborNode.GetTotalCost() > currentNode.GetTotalCost() + neighborNode.GetCost()) {

                                // The path to neighborNode through currentNode is cheaper than the current path
                                neighborNode.SetPrevTile(currentNode);
                            }
                        }
                        else {
                            Tile neighborNode = tiles[(int)neighbor.x, (int)neighbor.y].GetComponent<Tile>();

                            if (currentNode.GetTotalCost() + neighborNode.GetCost() <= range) {

                                // The node has not been added to either list and it is within the range limit
                                neighborNode.SetPrevTile(currentNode);
                                openTiles.Add(neighborNode);
                            }
                        }
                    }
                    else {
                        Tile neighborNode = closedTiles.Get(neighbor);

                        if (neighborNode.GetTotalCost() > currentNode.GetTotalCost() + neighborNode.GetCost()) {

                            //The path to neighborNode through currentNode is cheaper than the current path
                            neighborNode.SetPrevTile(currentNode);
                        }
                    }
                }
            });
        }

        return closedTiles;
    }
}