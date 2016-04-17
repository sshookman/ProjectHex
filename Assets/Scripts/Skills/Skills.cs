using UnityEngine;
using System.Collections;

/// <summary>
/// This class contains the list of skills for the Actor
/// </summary>
public class Skills : MonoBehaviour {

	[Tooltip("The skill buttons used in the HUD")]
	public GameObject skillButtonsPrefab;

	[Tooltip("The floating button to be used in the skill list")]
	public GameObject floatingButtonPrefab;

	private AbstractSkill[] skills;
	private GameObject skillButtons;

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
	/// Show the skill buttons for the Actor on the HUD
	/// </summary>
	public void ShowSkills() {
		if (!skillButtons) {
			skillButtons = (GameObject) Instantiate(skillButtonsPrefab, transform.position, transform.rotation);
			skillButtons.transform.SetParent(transform);

			Vector3 placementPosition = new Vector3(-0.5f, 1f);
			foreach (AbstractSkill skill in skills) {
				GameObject floatingButton = (GameObject) Instantiate(floatingButtonPrefab, placementPosition, transform.rotation);
				floatingButton.transform.SetParent(skillButtons.transform);

				placementPosition.Set(placementPosition.x, placementPosition.y - 1, placementPosition.z);
			}
		}
	}

	/// <summary>
	/// Hide the skill buttons HUD element if it exists for the Actor
	/// </summary>
	public void HideSkills() {
		if (skillButtons) {
			Destroy(skillButtons);
		}
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
