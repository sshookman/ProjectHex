using UnityEngine;

/// <summary>
/// Controls a highlight color for actors in the game to indicate damage, attack, or
/// other actions for an actor.
/// </summary>
public class ActorGlow : MonoBehaviour {

	private SpriteRenderer glow;
	private bool isGlowing;
	private float glowSpeed;
	private float glowCount;
	private int totalFlashes;
	private Color glowColor;

	/// <summary>
	/// Initializes variables and the SpriteRenderer compenent that is used to display
	/// the glow for the actor
	/// </summary>
	private void Start() {
		glow = GetComponent<SpriteRenderer>();
		isGlowing = false;
		glowSpeed = 0.15f;
		glowCount = 0;
		totalFlashes = 2;
	}

	/// <summary>
	/// If the glow is active the sprite renderer will flash a specified number of times
	/// before disabling
	/// </summary>
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

	/// <summary>
	/// Initiates a flash with a given color and a number of total flashes
	/// </summary>
	/// <param name="color">Color color</param>
	/// <param name="flashes">Flashes count</param>
	public void Flash(Color color, int flashes) {
		glowColor = color;
		totalFlashes = flashes * 2;
		glowCount = 0;
		isGlowing = true;
	}
}