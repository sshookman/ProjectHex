using UnityEngine;
using System.Collections.Generic;

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
	[Tooltip("The name of the actor")]
	public string actorName;

	private Health health;
	private Movement movement;
	private Attack attack;
	private AbstractSkill[] skills;

    /// <summary>
    /// Initializes the Actor with a name and maxHealth
    /// </summary>
    private void Start() {
		health = GetComponent<Health>();
		movement = GetComponent<Movement>();
		attack = GetComponent<Attack>();
		skills = GetComponents<AbstractSkill>();
    }

	/// <summary>
	/// Getter for the actor's health component
	/// </summary>
	/// <returns>The health component</returns>
	public Health GetHealth() {
		return health;
	}

	/// <summary>
	/// Getter for the actor's movement component
	/// </summary>
	/// <returns>The movement component</returns>
	public Movement GetMovement() {
		return movement;
	}

	/// <summary>
	/// Getter for the actor's attack component
	/// </summary>
	/// <returns>The attack component</returns>
	public Attack GetAttack() {
		return attack;
	}

	/// <summary>
	/// Getter for the actor's skills
	/// </summary>
	/// <returns>List of Skills</returns>
	public AbstractSkill[] GetSkills() {
		return skills;
	}

    /// <summary>
    /// Check to see if the actor is Skill Ready
    /// </summary>
    /// <returns>bool - Is Skill Ready</returns>
    public bool IsSkillReady() {
		foreach (AbstractSkill skill in skills) {
			if (!skill.IsReady()) {
				return false;
			}
		}

		return true;
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
