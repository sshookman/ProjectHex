using UnityEngine;
using System.Collections.Generic;

public class SpawnerAI : MonoBehaviour, GhoulAI {

	[Tooltip("The different ghoul prefabs that this spawner can spawn")]
	public GameObject[] ghouls;

	private GameManager gameManager;
	private int remainingGhouls;

	private void Start () {
		gameManager = GetComponentInParent<GameManager>();
	}

	public void Play() {
		Spawn();
	}

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
