using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// This script controls the individual hex tiles on the map. It controls
/// the glow color and tile options UI for the tile, and tracks any actor
/// placed on it.
/// </summary>
public class Tile : MonoBehaviour {

	[Tooltip("A blocking tile can not be accessed at all")]
	public bool isBlocking;
    [Tooltip("The number of speed units required to reach this tile")]
    [RangeAttribute(1, 10)]
    public int cost;
    [Tooltip("The type of terrain the tile represents")]
    public TileType type;
    [Tooltip("The GameObject responsible for making the tile glow")]
    public SpriteRenderer glow;
    [Tooltip("The Prefab that contains the options UI")]
    public GameObject tileOptionsPrefab;
    [Tooltip("The display image/sprite used in the HUD")]
    public Sprite displayImage;

    private int x;
    private int y;
    private Map map;
    private Tile prevTile;
    private TileState previousState;
    private TileState state;
    private GameObject tileOptions;
    
    /// <summary>
    /// Primary constructor for the HexTile
    /// </summary>
    /// <param name="x">HexTile x-coordinate</param>
    /// <param name="y">HexTile y-coordinate</param>
	public void Initialize(int x, int y) {
        this.x = x;
        this.y = y;
        map = GetComponentInParent<Map>();
        previousState = TileState.DEFAULT;
        state = TileState.DEFAULT;
    }

    /// <summary>
    /// Getter for the state of the tile
    /// </summary>
    /// <returns>current tile state</returns>
    public TileState GetState() {
        return state;
    }

    /// <summary>
    /// Setter for the state of the tile
    /// </summary>
    /// <param name="state">new tile state</param>
    public void SetState(TileState state) {
        this.state = state;
        if (state != previousState) {

            switch (state) {
                case TileState.DEFAULT:
                    glow.enabled = false;
                    if (previousState == TileState.SELECTED || previousState == TileState.LOCKED) {
                        if (previousState == TileState.SELECTED) {
                            Destroy(tileOptions);
                        }
                        HUDManager.setActor(null);
                        HUDManager.setTile(null);
                    }
                    previousState = state;
                    return;
                case TileState.HOVERED:
                    glow.color = Color.white;
                    glow.enabled = true;
                    previousState = state;
                    return;
                case TileState.SELECTED:
                    glow.color = Color.blue;
                    glow.enabled = true;
                    previousState = state;
                    HUDManager.setTile(this);
                    if (GetComponentInChildren<Actor>()) {
                        HUDManager.setActor(GetComponentInChildren<Actor>());
                        ShowtileOptions();
                    }
                    return;
                case TileState.REACHABLE:
                    glow.color = Color.cyan;
                    glow.enabled = true;
                    previousState = state;
                    return;
                case TileState.PATHWAY:
                    glow.color = Color.white;
                    glow.enabled = true;
                    previousState = state;
                    return;
                case TileState.ATTACKABLE:
                    glow.color = Color.red;
                    glow.enabled = true;
                    previousState = state;
                    return;
                case TileState.TARGET:
                    glow.color = Color.magenta;
                    glow.enabled = true;
                    previousState = state;
                    return;
                case TileState.LOCKED:
                    glow.color = Color.blue;
                    glow.enabled = true;
                    Destroy(tileOptions);
                    previousState = state;
                    return;
                case TileState.DISABLED:
                    glow.color = Color.black;
                    glow.enabled = true;
                    previousState = state;
                    return;
                default:
                    return;
            }
        }
    }

    /// <summary>
    /// Getter for X position on Map
    /// </summary>
    /// <returns>int - x-coordinate</returns>
    public int GetX() {
        return x;
    }

    /// <summary>
    /// Getter for Y position on Map
    /// </summary>
    /// <returns>int y-coordinate</returns>
    public int GetY() {
        return y;
    }

    /// <summary>
    /// Getter for the previous tile in the path
    /// </summary>
    /// <returns>HexTile - previous tile</returns>
    public Tile GetPrevTile() {
        return prevTile;
    }

    /// <summary>
    /// Setter for the previous tile in the path
    /// </summary>
    /// <param name="tile">previous tile</param>
    public void SetPrevTile(Tile tile) {
        this.prevTile = tile;
    }

    /// <summary>
    /// Getter for the tile's movement cost
    /// </summary>
    /// <returns>int - cost</returns>
    public int GetCost() {
		return (isBlocking) ? 999 : cost;
    }

    /// <summary>
    /// Getter for the total movement cost to get to this tile from
    /// the initial tile
    /// </summary>
    /// <returns>int - total cost</returns>
    public int GetTotalCost() {
        return (prevTile) ? prevTile.GetTotalCost() + GetCost() : 0;
    }

    /// <summary>
    /// Getter for the display details
    /// </summary>
    /// <returns>string - tile information</returns>
    public string getDisplayDetails() {
        return type.ToString();
    }

    /// <summary>
    /// Getter for the display image
    /// </summary>
    /// <returns>Sprite - display image</returns>
    public Sprite getDisplayImage() {
        return displayImage;
    }

    /// <summary>
    /// Check if the tile is blocked by some sort of obstacle
    /// </summary>
    /// <returns>bool - is blocked</returns>
    public bool IsBlocked() {
		if (isBlocking || GetComponentInChildren<Actor>()) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Getter for the Actor on the HexTile if one exists
    /// </summary>
    /// <returns>Actor - actor on tile or null if none exists</returns>
    public Actor GetActor() {
		return GetComponentInChildren<Actor>();
    }

	/// <summary>
	/// Getter for the Human on the tile if one exists
	/// </summary>
	/// <returns>Human - human on the tile or null if none exists</returns>
	public Human GetHuman() {
		return GetComponentInChildren<Human>();
	}

    /// <summary>
    /// Places an Actor on the tile as long as the tile is not already
    /// blocked
    /// </summary>
    /// <param name="actor">actor to be placed</param>
    /// <returns>bool - success</returns>
    public bool SetActor(GameObject actor) {

		if (!IsBlocked()) {
            actor.transform.parent = transform;
            actor.transform.position = transform.position;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Overloaded - Places an Actor on the tile as long as the tile is not already
    /// blocked
    /// </summary>
    /// <param name="actor">actor to be placed</param>
    /// <returns>bool - success</returns>
    public bool SetActor(Actor actor) {
        return SetActor(actor.gameObject);
    }

    /// <summary>
    /// Toggle the selected state of the tile (DEFAULT | SELECTED)
    /// </summary>
    public void ToggleSelect() {
        if (state == TileState.SELECTED) {
            SetState(TileState.DEFAULT);
        }
        else {
            SetState(TileState.SELECTED);
        }
    }

    /// <summary>
    /// Displays the tile options if the selected Actor is tagged a "Player"
    /// </summary>
    private void ShowtileOptions() {
        if (GetActor() && GetActor().tag.Equals("Player") && GetActor().HasOptions()) {
            tileOptions = (GameObject)Instantiate(tileOptionsPrefab, transform.position, transform.rotation);
            tileOptions.transform.SetParent(transform);
            TileOptionButton[] tileOptionButtons = tileOptions.GetComponentsInChildren<TileOptionButton>();

            for (int x = 0; x < tileOptionButtons.Length; x++) {
                tileOptionButtons[x].Initialize(GetActor());
            }
        }
    }

	/// <summary>
	/// Hides the tile options for the selected Actor
	/// </summary>
	public void HideTileOptions() {
		Destroy(tileOptions);
	}
    
	/// <summary>
	/// Attacks the actor on the tile with the selected actor
	/// </summary>
    public void Attack() {
        if (GetActor() && GetActor().tag == "Ghoul") {
			GetActor().GetHealth().Damage(10);
            if (map.HasSelectedActor()) {
                Actor attacker = map.GetSelectedActor();
				attacker.GetAttack().SetReady(false);
				attacker.GetMovement().SetSpeed(0);
            }
            map.SetState(MapState.SELECTION);
        }
    }

	/// <summary>
	/// Moves to the selected actor to the tile
	/// </summary>
    public void Move() {
        if (map.MoveSelectedActor(this)) {
            map.SetState(MapState.SELECTION);
        }
    }

	/// <summary>
	/// Select the tile
	/// </summary>
    public void Select() {
        map.SelectTile(x, y);
    }

	/// <summary>
	/// Highlights the path to this tile from the selected tile
	/// </summary>
    public void ShowPath() {
        SetState(TileState.PATHWAY);

        if (prevTile) {
            prevTile.ShowPath();
        }
    }

	/// <summary>
	/// Clears the highlighted path
	/// </summary>
    public void ClearPath() {
        map.SetReachableTileState(TileState.REACHABLE);
    }
}