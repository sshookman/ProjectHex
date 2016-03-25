using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This Controller handle the user input from the mouse for the Tiles on the
/// Map in order to update Status and trigger Attack, Move, and Select actions.
/// </summary>
public class TileInputController : MonoBehaviour {

    private Tile tile;
	private GameManager gameManager;

    /// <summary>
    /// Initializes the Tile component variable
    /// </summary>
    private void Start() {
        tile = GetComponent<Tile>();
		gameManager = GetComponentInParent<GameManager>();
    }

    /// <summary>
    /// Handle the tile state changes when the mouse is hovering over the tile
    /// </summary>
    private void OnMouseOver() {

		if (gameManager.GetState() == GameState.HUMAN_TURN) {
	        bool isOverButton = EventSystem.current.IsPointerOverGameObject();
	        switch (tile.GetState()) {
	            case TileState.DEFAULT:
	                if (!isOverButton) {
	                    tile.SetState(TileState.HOVERED);
	                }
	                return;
	            case TileState.HOVERED:
	                if (isOverButton) {
	                    tile.SetState(TileState.DEFAULT);
	                }
	                return;
	            case TileState.REACHABLE:
	                if (!isOverButton) {
	                    tile.ShowPath();
	                }
	                return;
	            case TileState.ATTACKABLE:
	                if (!isOverButton) {
	                    tile.SetState(TileState.TARGET);
	                }
	                return;
	            default:
	                return;
	        }
		}
    }

    /// <summary>
    /// Handle the tile state changes when the mouse leaves the tile
    /// </summary>
    private void OnMouseExit() {

		if (gameManager.GetState() == GameState.HUMAN_TURN) {
	        switch (tile.GetState()) {
	            case TileState.HOVERED:
	                tile.SetState(TileState.DEFAULT);
	                return;
	            case TileState.PATHWAY:
	                tile.ClearPath();
	                return;
	            case TileState.TARGET:
	                tile.SetState(TileState.ATTACKABLE);
	                return;
	            default:
	                return;
	        }
		}
    }

    /// <summary>
    /// Handle the tile state changes when the mouse clicks on the tile
    /// </summary>
    private void OnMouseDown() {

		if (gameManager.GetState() == GameState.HUMAN_TURN) {
	        bool isOverButton = EventSystem.current.IsPointerOverGameObject();
	        switch (tile.GetState()) {
	            case TileState.LOCKED:
	            case TileState.DISABLED:
	                return;
	            case TileState.ATTACKABLE:
	            case TileState.TARGET:
	                if (!isOverButton) {
	                    tile.Attack();
	                }
	                return;
	            case TileState.REACHABLE:
	            case TileState.PATHWAY:
	                if (!isOverButton) {
	                    tile.Move();
	                }
	                return;
	            default:
	                if (!isOverButton) {
	                    tile.Select();
	                }
	                return;
	        }
		}
    }
}
