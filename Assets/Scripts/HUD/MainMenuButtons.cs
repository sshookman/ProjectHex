using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls the buttons on the main menu screen of the game.
/// </summary>
public class MainMenuButtons : MonoBehaviour {

	/// <summary>
	/// When the "Start" button is clicked the Forest Map is loaded and the
	/// game begins
	/// </summary>
	public void ClickStart() {
		SceneManager.LoadScene("ForestMap");
	}

	/// <summary>
	/// When the "Options" button is clicked the options menu is displayed
	/// NOT YET IMPLEMENTED
	/// </summary>
	public void ClickOptions() {
		Debug.Log("Options not yet implemented...");
	}

	/// <summary>
	/// When the "Exit" button is clicked the game is closed
	/// </summary>
	public void ClickExit() {
		Application.Quit();
	}
}
