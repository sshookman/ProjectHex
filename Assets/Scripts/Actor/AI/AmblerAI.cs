using UnityEngine;
using System.Collections;

/// <summary>
/// This script is used to handle the AI behavior of Ambler Ghoul
/// Actors to wander around the map and attack nearby Humans.
/// </summary>
public class AmblerAI : MonoBehaviour, GhoulAI {

    private Map map;
    private Tile tile;
    private Actor amblerActor;
    private Actor target;

    /// <summary>
    /// Initializes the Map component variable
    /// </summary>
    private void Start() {
        map = GetComponentInParent<Map>();
        amblerActor = GetComponent<Actor>();
    }

	public void Play() {
		Move();
		Attack();
	}

    /// <summary>
    /// Moves the Ambler aimlessly around the map if there is no target
    /// available. If there is a target, then move towards the target.
    /// </summary>
	private void Move() {
		tile = GetComponentInParent<Tile>();
		Vector2 tileVector = new Vector2(tile.GetX(), tile.GetY());
		Scan(tileVector);

		TileList reachableTiles = MapUtil.BuildReachableTiles(map.GetTiles(), tileVector, map.width, map.height, amblerActor.GetMovement().GetSpeed(), false);
		Tile destination = (target) ? reachableTiles.GetClosest(target.GetComponentInParent<Tile>().GetX(), target.GetComponentInParent<Tile>().GetY()) : reachableTiles.GetRandom(map.width, map.height);
        destination.SetActor(amblerActor);
    }

    /// <summary>
    /// Attacks the selected target if they are within range
    /// </summary>
	private void Attack() {
		if (target) {
			tile = GetComponentInParent<Tile>();
			Vector2 targetVector = new Vector2(target.GetComponentInParent<Tile>().GetX(), target.GetComponentInParent<Tile>().GetY());
			Vector2 amblerVector = new Vector2(tile.GetX(), tile.GetY());
			if (MapUtil.IsNeighbor(targetVector, amblerVector)) {
				target.GetHealth().Damage(5);
			}
		}
    }

	/// <summary>
	/// Scans for a target Player if no target is currently set
	/// </summary>
	/// <param name="tileVector">current position vector</param>
	private void Scan(Vector2 tileVector) {
		if (!target) {
			TileList visibleTiles = MapUtil.BuildReachableTiles(map.GetTiles(), tileVector, map.width, map.height, amblerActor.GetMovement().GetSpeed() * 2, true);
			visibleTiles.GetTiles().ForEach(delegate (Tile visibleTile) {
				if (visibleTile.GetActor() && visibleTile.GetActor().tag.Equals("Player")) {
					target = visibleTile.GetActor();
				}
			});
		}
	}
}