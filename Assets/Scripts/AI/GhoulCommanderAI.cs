using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script acts as a high level AI to manage the ghoul turn 
/// for the game. It initiates the Play action for all ghouls on
/// the map one-by-one.
/// </summary>
public class GhoulCommanderAI : MonoBehaviour {

	private GameManager gameManager;
	private Map map;
	private CameraInputController cameraInputController;
	private List<Actor>.Enumerator ghoulEnumerator;
	private List<Actor>.Enumerator spawnerEnumerator;
	private float timer;
	private bool isGhoulTurn;
	private bool isGhoulsRemaining;
	private bool isSpawnersRemaining;

	/// <summary>
	/// Initilizes variables for the Game Manager and the Map
	/// </summary>
	private void Start () {
		gameManager = GetComponent<GameManager>();
		map = GetComponentInChildren<Map>();
		cameraInputController = GetComponentInChildren<CameraInputController>();
	}

	/// <summary>
	/// Iterates through the list of ghouls and spawners to initiate the Play
	/// action on each. Player inputs are disabled during the ghoul turn.
	/// </summary>
	private void Update() {
		if (isGhoulTurn) {
			timer += Time.deltaTime;
			if (timer >= 1f) {
				if (isGhoulsRemaining) {
					map.SetState(MapState.GHOULS);
					cameraInputController.SetTarget(ghoulEnumerator.Current.transform);
					ghoulEnumerator.Current.Reset();
					ghoulEnumerator.Current.GetComponent<GhoulAI>().Play();
					map.SetState(MapState.SELECTION);
					isGhoulsRemaining = ghoulEnumerator.MoveNext();
				} else if (isSpawnersRemaining) {
					map.SetState(MapState.GHOULS);
					cameraInputController.SetTarget(spawnerEnumerator.Current.transform);
					spawnerEnumerator.Current.Reset();
					spawnerEnumerator.Current.GetComponent<GhoulAI>().Play();
					map.SetState(MapState.SELECTION);
					isSpawnersRemaining = spawnerEnumerator.MoveNext();
				} else {
					cameraInputController.SetTarget(null);
					isGhoulTurn = false;
					gameManager.GetHumans().ForEach(delegate (Actor human) {
						human.Reset();
					});
					gameManager.SetState(GameState.HUMAN_TURN);
				}
				timer = 0f;
			}
		}
	}

	/// <summary>
	/// Plays the Ghoul turn by moving and attacking and then 
	/// resetting the human actors before the human turn is 
	/// started
	/// </summary>
	public void StartGhoulTurn() {
		ghoulEnumerator = gameManager.GetGhouls().GetEnumerator();
		isGhoulsRemaining = ghoulEnumerator.MoveNext();
		spawnerEnumerator = gameManager.GetSpawners().GetEnumerator();
		isSpawnersRemaining = spawnerEnumerator.MoveNext();
		timer = 0f;
		isGhoulTurn = true;
	}
}
