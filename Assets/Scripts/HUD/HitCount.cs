using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Controls the display for the numric hit count that appears
/// above damaged or healed actors
/// </summary>
public class HitCount : MonoBehaviour {

	private Text hitText;
	private int hitCount;
	private bool isFading;

	/// <summary>
	/// Initializes the Text component, text, and fade for the
	/// display
	/// </summary>
	private void Start () {
		hitText = GetComponentInChildren<Text>();
		hitText.text = "";
		isFading = false;
	}

	/// <summary>
	/// Displays the hit count on the screen, slowly fades out,
	/// and finally destroys itself
	/// </summary>
	private void Update () {

		if (hitText.text.Equals("") && hitCount > 0) {
			hitText.text = "-" + hitCount;
			isFading = true;
		}

		if (isFading) {
			Color hitTextColor = hitText.color;
			hitText.color = new Color(hitTextColor.r, hitTextColor.g, hitTextColor.b, hitTextColor.a - 0.02f);
		}

		if (hitText.color.a <= 0) {
			Destroy(this.gameObject);
		}
	}

	/// <summary>
	/// Setter for the hit count value to be displayed
	/// </summary>
	/// <param name="hitCount">Hit count int</param>
	public void SetHitCount(int hitCount) {
		this.hitCount = hitCount;
	}
}
