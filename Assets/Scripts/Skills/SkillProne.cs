using UnityEngine;

/// <summary>
/// Prone skill allows the player to lay down and take
/// better aim at targets, thus improving range and
/// damage to targets.
/// </summary>
public class SkillProne : AbstractSkill {

	/// <summary>
	/// Initializes the actor component that is the
	/// owner of the skill
	/// </summary>
	public void Start() {
		skillName = "Prone";
		cost = 4;
		isReady = true;
	}

	/// <summary>
	/// Activates the skill for the parent Actor
	/// </summary>
	public void Activate() {
		isReady = false;
		//TODO: Go to attack mode to kneecap target
	}
}
