using UnityEngine;
using System.Collections;

/// <summary>
/// This class contains the list of skills for the Actor
/// </summary>
public class Skills : MonoBehaviour {

	private AbstractSkill[] skills;

	/// <summary>
	/// Initializes the array of skills
	/// </summary>
	public void Start() {
		skills = GetComponents<AbstractSkill>();
	}

	/// <summary>
	/// Check to see if there are any skills that are ready
	/// </summary>
	/// <returns>bool - Is Ready</returns>
	public bool IsReady() {
		bool isReady = false;
		if (skills != null || !skills.Length.Equals(0)) {
			foreach (AbstractSkill skill in skills) {
				isReady = (skill.IsReady()) ? skill.IsReady() : isReady;
			}
		}

		return isReady;
	}

	/// <summary>
	/// Resets all of the skills
	/// </summary>
	public void Reset() {
		if (skills != null || !skills.Length.Equals(0)) {
			foreach (AbstractSkill skill in skills) {
				skill.Reset();
			}
		}
	}
}
