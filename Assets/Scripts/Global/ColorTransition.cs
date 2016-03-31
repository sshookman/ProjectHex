using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorTransition : MonoBehaviour {

	[Tooltip("The speed at which the color will change")]
	[Range(1, 10)]
	public int speed;

	private Image image;
	private float red;
	private float green;
	private float blue;
	private float redSpeed;
	private float greenSpeed;
	private float blueSpeed;
	private float speedMultiplier;

	void Start () {
		image = GetComponent<Image>();
		red = 1f;
		green = 0f;
		blue = 0f;
		speedMultiplier = 0.001f;
	}

	void Update () {

		image.color = new Color(red, green, blue);

		if (red >= 1f && green <= 0f && blue <= 0f) {
			redSpeed = 0f;
			blueSpeed = 0f;
			greenSpeed = speed * speedMultiplier;
		} else if (red >= 1f && green >= 1f && blue <= 0f) {
			greenSpeed = 0f;
			blueSpeed = 0f;
			redSpeed = speed * speedMultiplier * -1f;
		} else if (red <= 0f && green >= 1f && blue <= 0f) {
			redSpeed = 0f;
			greenSpeed = 0f;
			blueSpeed = speed * speedMultiplier;
		} else if (red <= 0f && green >= 1f && blue >= 1f) {
			redSpeed = 0f;
			blueSpeed = 0f;
			greenSpeed = speed * speedMultiplier * -1f;
		} else if (red <= 0f && green <= 0f && blue >= 1f) {
			greenSpeed = 0f;
			blueSpeed = 0f;
			redSpeed = speed * speedMultiplier;
		} else if (red >= 1f && green <= 0f && blue >= 1f) {
			redSpeed = 0f;
			greenSpeed = 0f;
			blueSpeed = speed * speedMultiplier * -1f;
		}

		red += redSpeed;
		green += greenSpeed;
		blue += blueSpeed;
	}
}
