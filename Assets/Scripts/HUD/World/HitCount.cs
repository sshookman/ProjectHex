using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitCount : MonoBehaviour {

	private int hitCount;
	private Text hitText;
	private bool isFading;

	private void Start () {
		hitText = GetComponentInChildren<Text>();
		hitText.text = "";
		isFading = false;

		//TEMP
		hitCount = 10;
	}
	
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

	public void SetHitCount(int hitCount) {
		this.hitCount = hitCount;
	}
}
