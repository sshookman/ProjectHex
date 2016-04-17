using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to generate a randomized map of hex tiles with
/// humans and ghouls already populated.It also maintains a list of tiles
/// in the map to control which is tile is selected, and how they glow
/// for ATTACK or MOVE modes.
/// </summary>
public class Map : MonoBehaviour {

    [Tooltip("Human Actor Prefabs")]
    public GameObject[] humans;

	[Tooltip("Ghoul Spawner Prefabs")]
	public GameObject[] spawners;
    [Tooltip("Ghoul Actor Prefabs")]
    public GameObject[] ghouls;
	[Tooltip("The number of ghouls to place on the map")]
	public int ghoulLimit;

	[Tooltip("The standard tile prefab with which the map will be constructed")]
	public GameObject tile;
	[Tooltip("The tree tile prefab with which the map will be constructed")]
	public GameObject tree;
    [Tooltip("Number of tiles for the width of the map")]
    [Range(10, 50)]
    public int width;
    [Tooltip("Number of tiles for the height of the map")]
    [Range(10, 50)]
    public int height;

	private GameManager gameManager;
    private GameObject[,] tiles;
    private Vector2 selected;
    private GameState previousGameState;
    private MapState previousState;
    private MapState state;
    private TileList reachableTiles;

    /// <summary>
    /// Initializes the Map with an array of Tiles based on the input
    /// </summary>
    private void Start () {
		gameManager = GetComponentInParent<GameManager>();
		previousGameState = gameManager.GetState();
        state = MapState.SELECTION;
        selected = new Vector2(-1, -1);
        tiles = new GameObject[width, height];
		BuildRandomMap();
		gameManager.SetState(GameState.HUMAN_TURN);
    }

    /// <summary>
    /// Checks if the Game State has changed to a different turn
    /// and updates the Tiles on the map accordingly
    /// </summary>
    private void Update () {
		if (previousGameState != gameManager.GetState()) {
			previousGameState = gameManager.GetState();
			if (gameManager.GetState() == GameState.GHOUL_TURN) {
				ResetTiles();
            } else {
                ResetTiles();
            }
        }
    }

	/// <summary>
	/// Check if the given x and y coordinates fall within the bounds of the map
	/// </summary>
	/// <returns><c>true</c> if this instance has tile the specified x y; otherwise, <c>false</c>.</returns>
	/// <param name="x">The x coordinate</param>
	/// <param name="y">The y coordinate</param>
	public bool HasTile(int x, int y) {
		return (x >= 0 && x < width && y >= 0 && y < height);
	}

	/// <summary>
	/// Check if the given Vector2 coordinate falls within the bounds of the map
	/// </summary>
	/// <returns><c>true</c> if this instance has tile the specified coord; otherwise, <c>false</c>.</returns>
	/// <param name="coord">Coordinate Vector2</param>
	public bool HasTile(Vector2 coord) {
		return HasTile((int)coord.x, (int)coord.y);
	}

    /// <summary>
    /// Getter for the state of the map
    /// </summary>
    /// <returns>MapState - current state</returns>
    public MapState GetState() {
        return state;
    }

    /// <summary>
    /// Setter for state of the map
    /// </summary>
    /// <param name="state">The new state of the map</param>
    public void SetState(MapState state) {
        this.state = state;
        if (state != previousState) {

            previousState = state;
            switch (state) {
                case MapState.SELECTION:
                    ResetTiles();
                    return;
                case MapState.MOVEMENT:
					int range = tiles[(int)selected.x, (int)selected.y].GetComponent<Tile>().GetActor().GetMovement().GetSpeed();
                    DisableTiles();
                    BuildReachableTiles(range, false);
                    SetReachableTileState(TileState.REACHABLE);
                    return;
                case MapState.ATTACK:
					int maxWeaponRange = tiles[(int)selected.x, (int)selected.y].GetComponent<Tile>().GetHuman().GetMaxWeaponRange();
					int minWeaponRange = tiles[(int)selected.x, (int)selected.y].GetComponent<Tile>().GetHuman().GetMinWeaponRange();
                    DisableTiles();
                    BuildReachableTiles(maxWeaponRange, true);
                    SetReachableTileState(minWeaponRange, TileState.ATTACKABLE);
                    return;
				case MapState.SKILL:
					if (HasSelectedActor()) {
						GetSelectedActor().GetSkills().ShowSkills();
					}
					return;
                default:
                    return;
            }
        }
    }

    /// <summary>
    /// Getter for the Actor on the selected tile in the map
    /// </summary>
    /// <returns>Actor - selected actor</returns>
    public Actor GetSelectedActor() {
        if (selected.x != -1 && selected.y != -1) {
            return tiles[(int)selected.x, (int)selected.y].GetComponent<Tile>().GetActor();
        }
        return null;
    }

    /// <summary>
    /// Checks to see if an Actor is present on the selected tile
    /// </summary>
    /// <returns>bool - actor is present</returns>
    public bool HasSelectedActor() {
        return (selected.x != -1 && selected.y != -1 &&
            tiles[(int)selected.x, (int)selected.y].GetComponent<Tile>() &&
            tiles[(int)selected.x, (int)selected.y].GetComponent<Tile>().GetActor());
    }

    /// <summary>
    /// Move the currently selected Actor, if one is selected, to a new Tile
    /// </summary>
    /// <param name="tile">The new Tile to which the Actor will be moved</param>
    /// <returns>bool - Success</returns>
    public bool MoveSelectedActor(Tile tile) {
        if (selected.x != -1 && selected.y != -1) {
            Actor actor = tiles[(int)selected.x, (int)selected.y].GetComponent<Tile>().GetActor();
            if (actor) {
				actor.GetMovement().SetSpeed(actor.GetMovement().GetSpeed() - tile.GetTotalCost());
                tile.SetActor(actor);
                return true;
            }
        }
        return false;
    }

	/// <summary>
	/// Return the GameObjects for all the tiles on the Map
	/// </summary>
	/// <returns>The tiles.</returns>
    public GameObject[,] GetTiles() {
        return tiles;
    }

    /// <summary>
    /// Selects the given Tile in the map and de-selects the old Tile
    /// </summary>
    /// <param name="x">x-coordinate of the selected tile</param>
    /// <param name="y">y-coordinate of the selected tile</param>
    public void SelectTile(int x, int y) {

        if (selected.x != -1 && selected.y != -1) {

            // Toggle Select on Old Hex Tile
            GameObject oldSelected = tiles[(int)selected.x, (int)selected.y];
            oldSelected.GetComponent<Tile>().ToggleSelect();
        }

        if (selected.x != x || selected.y != y) {

			// Toggle Select on New Hex Tile
            selected = new Vector2(x, y);
            GameObject newSelected = tiles[x, y];
            newSelected.GetComponent<Tile>().ToggleSelect();
        } else {

            // Reset Selected to Nothing
            selected = new Vector2(-1, -1);
        }
    }

    /// <summary>
    /// Set the state of each of the reachable tiles to 'REACHABLE'
    /// </summary>
    public void SetReachableTileState(TileState state) {
        SetReachableTileState(0, state);
    }

    /// <summary>
    /// Set the state of each of the reachable tiles to 'REACHABLE' if it is
    /// above the min range
    /// </summary>
    public void SetReachableTileState(int minRange, TileState state) {
        reachableTiles.GetTiles().ForEach(delegate (Tile tile) {
            if (tile.GetTotalCost() > minRange) {
                tile.SetState(state);
            }
        });
    }

	/// <summary>
	/// Instantiates the GameObjects for the map and the actors based on a
	/// randomized template.
	/// </summary>
	private void BuildRandomMap() {
		int[,] mapTemplate = MapBuilderUtil.GenerateForestMap(width, height);
		int placedGhouls = 0;
		int placedHumans = 0;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				Vector3 position = transform.position;
				position.x += (x * .90f);
				position.y += (y * 1.11f) + (x % 2 * .55f);
				if (mapTemplate[x, y] >= MapBuilderUtil.TILE) {

					GameObject newTile = (GameObject) Instantiate(tile, position, transform.rotation);
					newTile.transform.parent = transform;
					newTile.GetComponent<Tile>().Initialize(x, y);
					tiles[x, y] = newTile;

					if (mapTemplate[x, y] == MapBuilderUtil.TILE + MapBuilderUtil.GHOUL && placedGhouls < ghoulLimit) {
						GameObject actor = (GameObject) Instantiate(ghouls[UnityEngine.Random.Range(0, ghouls.Length)], transform.position, transform.rotation);
						Tile hexTile = newTile.GetComponent<Tile>();
						hexTile.SetActor(actor);
						gameManager.AddGhoul(actor.GetComponent<Actor>());
						placedGhouls++;
					} else if (mapTemplate[x, y] == MapBuilderUtil.TILE + MapBuilderUtil.HUMAN && placedHumans < humans.Length) {
						GameObject actor = (GameObject) Instantiate(humans[placedHumans], transform.position, transform.rotation);
						Tile hexTile = newTile.GetComponent<Tile>();
						hexTile.SetActor(actor);
						gameManager.AddHuman(actor.GetComponent<Actor>());
						placedHumans++;
					} else if (mapTemplate[x, y] == MapBuilderUtil.TILE + MapBuilderUtil.SPAWN) {
						GameObject actor = (GameObject) Instantiate(spawners[UnityEngine.Random.Range(0, spawners.Length)], transform.position, transform.rotation);
						Tile hexTile = newTile.GetComponent<Tile>();
						hexTile.SetActor(actor);
						gameManager.AddSpawner(actor.GetComponent<Actor>());
					}

				} else {
					GameObject newTile = (GameObject) Instantiate(tree, position, transform.rotation);
					newTile.transform.parent = transform;
					newTile.GetComponent<Tile>().Initialize(x, y);
					tiles[x, y] = newTile;
				}
			}
		}
	}

    /// <summary>
    /// Disable all of the tiles so no tile can be selected, except
    /// for the tile that is already selected
    /// </summary>
    private void DisableTiles() {
        Tile[] allTiles = GetComponentsInChildren<Tile>();
        
        for (int x = 0; x < allTiles.Length; x++) {
            if (allTiles[x].GetState() != TileState.SELECTED) {
                allTiles[x].SetState(TileState.DISABLED);
            } else {
                allTiles[x].SetState(TileState.LOCKED);
            }
        }
    }

    /// <summary>
    /// Obtain the list of all tiles that are reachable from the selected tile
    /// on the map
    /// </summary>
    private void BuildReachableTiles(int range, bool ignoreBlockers) {

        reachableTiles = MapUtil.BuildReachableTiles(tiles, selected, width, height, range, ignoreBlockers); ;
    }

    /// <summary>
    /// Resets all of the tile on the map to default state so they
    /// can be selected
    /// </summary>
    private void ResetTiles() {
		if (HasSelectedActor()) {
			GetSelectedActor().Reset();
		}

        Tile[] allTiles = GetComponentsInChildren<Tile>();
        selected = new Vector2(-1, -1);

        for (int x = 0; x < allTiles.Length; x++) {
            allTiles[x].SetState(TileState.DEFAULT);
            allTiles[x].SetPrevTile(null);   
        }
    }
}