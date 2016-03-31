using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	public void ClickStart() {
		SceneManager.LoadScene("ForestMap");
	}

	public void ClickOptions() {
		Debug.Log("Options not yet implemented...");
	}

	public void ClickExit() {
		Application.Quit();
	}
}
