using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	[Tooltip("The starting health and maximum health the actor can reach")]
	[Range(10, 100)]
	public int maxHealth;

	[Tooltip("The prefab for UI display of damage and healing")]
	public GameObject hitCount;

	private GameManager gameManager;
	private ActorGlow glow;
	private int health;

	private void Start () {
		gameManager = GetComponentInParent<GameManager>();
		glow = GetComponentInChildren<ActorGlow>();
		health = maxHealth;
	}

	/// <summary>
	/// Getter for the Actor's current health
	/// </summary>
	/// <returns>int - current health</returns>
	public int GetHealth() {
		return health;
	}

	/// <summary>
	/// Calculates damage based on defense and applies it to the Actor
	/// </summary>
	/// <param name="damage">int - attack damage</param>
	public void Damage(int damage) {
		ShowHitCount(damage);
		glow.Flash(Color.red, 3);
		health -= damage;
		health = (health < 0) ? 0 : health;
		if (health == 0) {
			gameManager.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
	}

	/// <summary>
	/// Heals the Human Actor without exceeding maxHealth
	/// </summary>
	/// <param name="healed">int - heal amount</param>
	public void Heal(int healed) {
		health += healed;
		health = (health > maxHealth) ? maxHealth : health;
	}

	private void ShowHitCount(int hitValue) {
		Vector2 position = transform.position;
		position.y += 0.5f;
		GameObject hitCountGO = (GameObject) Instantiate(hitCount, position, transform.rotation);
		hitCountGO.GetComponent<HitCount>().SetHitCount(hitValue);
	}
}
