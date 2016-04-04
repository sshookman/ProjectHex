using UnityEngine;
using System.Collections;

/// <summary>
/// This is an interface for the skills that Actors can use during
/// gameplay.
/// </summary>
public interface Skill : MonoBehaviour {

	/// <summary>
	/// Getter for the cost of the skill being used
	/// </summary>
	/// <returns>The skill's cost</returns>
	int GetCost();

	/// <summary>
	/// Activates the skill for the parent Actor
	/// </summary>
	void Activate();
}
