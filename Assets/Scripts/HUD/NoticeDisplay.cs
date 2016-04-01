using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// This script manages the UI display elements for the notice messages. It
/// is responsible for indicating the turn transitions.
/// </summary>
public class NoticeDisplay : MonoBehaviour {

	public GameManager gameManager;
    private GameState oldState;
    private Image centerPanel;
    private Text noticeText;

	/// <summary>
	/// Initilizes the game state and the center panel and text 
	/// components
	/// </summary>
	private void Start () {
		oldState = gameManager.GetState();
        centerPanel = GetComponent<Image>();
        noticeText = GetComponentInChildren<Text>();
	}

	/// <summary>
	/// Displays a notice of turn change if the game state
	/// changes
	/// </summary>
	private void Update () {
		if (gameManager.GetState() != oldState) {
			oldState = gameManager.GetState();
			StartCoroutine(ShowNotice((oldState.Equals(GameState.DEFEAT) || oldState.Equals(GameState.VICTORY))));
        }
	}

	/// <summary>
	/// Displays a notice on the center panel with the current game
	/// state
	/// </summary>
	/// <returns>IEnumerator</returns>
	private IEnumerator ShowNotice(bool isGameOver) {
        centerPanel.enabled = true;
        noticeText.enabled = true;
		noticeText.text = gameManager.GetState().ToString().Replace("_", " ");
        yield return new WaitForSeconds(3);
        noticeText.enabled = false;
        centerPanel.enabled = false;
		if (isGameOver) {
			SceneManager.LoadScene("MainMenu");
		}
    }
}
