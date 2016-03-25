using UnityEngine;
using System.Collections;

/// <summary>
/// Controller for the gameplay input such as menu screens and turn transitions
/// </summary>
public class GameInputController : MonoBehaviour {

	private GameManager gameManager;

	/// <summary>
	/// Initializes the game manager variable
	/// </summary>
	private void Start () {
		gameManager = GetComponent<GameManager>();
	}

	/// <summary>
	/// Handles input for the space key to end the human turn
	/// </summary>
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && gameManager.GetState() == GameState.HUMAN_TURN) {
			gameManager.SetState(GameState.GHOUL_TURN);
		}
	}
}
