using UnityEngine;

public class AbstractSkill : MonoBehaviour {

	protected string skillName;
	protected int cost;
	protected bool isReady;

	/// <summary>
	/// Getter for the name of the skill
	/// </summary>
	/// <returns>The skill's name</returns>
	public string GetName() {
		return skillName;
	}

	/// <summary>
	/// Getter for the cost of the skill
	/// </summary>
	/// <returns>The skill's cost</returns>
	public int GetCost() {
		return cost;
	}

	/// <summary>
	/// Determines whether this skill is ready to use
	/// </summary>
	/// <returns><c>true</c> if this skill is ready; otherwise, <c>false</c>.</returns>
	public bool IsReady() {
		return isReady;
	}

	/// <summary>
	/// Activates the skill for the parent Actor
	/// </summary>
	public void Activate() {
		isReady = false;
	}

	/// <summary>
	/// Reset this skill
	/// </summary>
	public void Reset() {
		isReady = true;
	}
}
