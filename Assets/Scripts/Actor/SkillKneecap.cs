using UnityEngine;

/// <summary>
/// Kneecap skill that deals damage and has a chance to cripple
/// the oponent and force them to crawl.
/// </summary>
public class SkillKneecap : AbstractSkill {

	private string skillName;
	private int cost;
	private bool isReady;

	/// <summary>
	/// Initializes the actor component that is the
	/// owner of the skill
	/// </summary>
	public void Start() {
		skillName = "Kneecap";
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
