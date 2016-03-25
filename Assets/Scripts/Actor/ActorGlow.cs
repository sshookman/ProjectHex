using UnityEngine;

public class ActorGlow : MonoBehaviour {

	private SpriteRenderer glow;
	private bool isGlowing;
	private float glowSpeed;
	private float glowCount;

	private int totalFlashes;
	private Color glowColor;

	private void Start() {
		glow = GetComponent<SpriteRenderer>();
		isGlowing = false;
		glowSpeed = 0.15f;
		glowCount = 0;
		totalFlashes = 2;
	}

	private void FixedUpdate() {

		if (isGlowing) {
			glow.color = new Color(glowColor.r, glowColor.g, glowColor.b, glow.color.a + glowSpeed);
			if (glow.color.a >= 1 || glow.color.a <= 0) {
				glowSpeed *= -1;
				glowCount++;
			}
			if (glowCount >= totalFlashes) {
				isGlowing = false;
				glow.color = new Color(1f, 1f, 1f, 0f);
			}
		}
	}

	public void Flash(Color color, int flashes) {
		glowColor = color;
		totalFlashes = flashes * 2;
		glowCount = 0;
		isGlowing = true;
	}
}