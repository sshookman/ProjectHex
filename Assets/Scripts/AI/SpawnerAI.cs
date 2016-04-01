using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This scripts handles the behavior of the Spawner Actors which
/// generates a specified number of ghouls on the map before disappearing.
/// </summary>
public class SpawnerAI : MonoBehaviour, GhoulAI {

	[Tooltip("The different ghoul prefabs that this spawner can spawn")]
	public GameObject[] ghouls;

	private GameManager gameManager;
	private int remainingGhouls;

	/// <summary>
	/// Initializes the Game Manager
	/// </summary>
	private void Start () {
		gameManager = GetComponentInParent<GameManager>();
	}

	/// <summary>
	/// Initiates the Spawner's turn by generating a new ghoul
	/// on the map
	/// </summary>
	public void Play() {
		Spawn();
	}

	/// <summary>
	/// Spawns a new ghoul on the map on a tile next to the Spawner
	/// Actor
	/// </summary>
	private void Spawn() {

		Tile tile = GetComponentInParent<Tile>();
		Map map = GetComponentInParent<Map>();
		List<Vector2> neighbors = MapUtil.GetNeighbors(tile.GetX(), tile.GetY());
		tile = null;
		neighbors.ForEach(delegate (Vector2 neighbor){
			if (map.HasTile(neighbor) && !map.GetTiles()[(int)neighbor.x, (int)neighbor.y].GetComponent<Tile>().IsBlocked()) {
				tile = map.GetTiles()[(int)neighbor.x, (int)neighbor.y].GetComponent<Tile>();
			}
		});

		if (tile) {
			GameObject ghoul = (GameObject) Instantiate(ghouls[UnityEngine.Random.Range(0, ghouls.Length)], transform.position, transform.rotation);
			tile.SetActor(ghoul);
			remainingGhouls--;
			gameManager.AddGhoul(ghoul.GetComponent<Actor>());
		}
	}
}
