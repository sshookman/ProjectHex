using UnityEngine;

/// <summary>
/// This script provides properties for an actor such as health, speed, defense, weapons, 
/// etc, and a name used for reference and display purposes.
/// </summary>
public class Actor : MonoBehaviour {

    [Tooltip("The defensive strength of the actor")]
    [Range(0, 50)]
    public int defense;
    [Tooltip("The display image/sprite used in the HUD")]
	public Sprite displayImage;
	public string actorName;

	private Health health;
	private Movement movement;
	private Attack attack;

    /// <summary>
    /// Initializes the Actor with a name and maxHealth
    /// </summary>
    private void Start() {
		health = GetComponent<Health>();
		movement = GetComponent<Movement>();
		attack = GetComponent<Attack>();
    }

	public Health GetHealth() {
		return health;
	}

	public Movement GetMovement() {
		return movement;
	}

	public Attack GetAttack() {
		return attack;
	}

    /// <summary>
    /// Check to see if the actor is Skill Ready
    /// </summary>
    /// <returns>bool - Is Skill Ready</returns>
    public bool IsSkillReady() {
        return false;
    }

    /// <summary>
    /// Check to see if the actor has any options (Move, Attack, Skill)
    /// </summary>
    /// <returns>bool - has options</returns>
    public bool HasOptions() {
		return (movement.IsReady() || attack.IsReady() || IsSkillReady());
    }

    /// <summary>
    /// Getter for the HUD display details of the actor
    /// </summary>
    /// <returns>string - actor information</returns>
    public string getDisplayDetails() {
		return actorName + "\n" + health.GetHealth() + "/" + health.maxHealth;
    }

    /// <summary>
    /// Getter for the HUD display image of the actor
    /// </summary>
    /// <returns>Sprite - actor image</returns>
    public Sprite getDisplayImage() {
        return displayImage;
    }

    /// <summary>
    /// Resets the Actor's speed and attack readiness
    /// </summary>
    public void Reset() {
		if (movement) {
			movement.Reset();
		}
		if (attack) {
			attack.Reset();
		}
    }

}
