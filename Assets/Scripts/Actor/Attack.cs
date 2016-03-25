using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private bool isAttackReady;

	private void Start () {
		isAttackReady = true;
	}

	/// <summary>
	/// Check to see if the actor is Attack Ready
	/// </summary>
	/// <returns>bool - Is Attack Ready</returns>
	public bool IsReady() {
		return isAttackReady;
	}

	/// <summary>
	/// Setter for the isAttackReady flag on the actor that indicates the actor
	/// can engage in combat
	/// </summary>
	/// <param name="attackReady">bool for attack readiness</param>
	public void SetReady(bool attackReady) {
		isAttackReady = attackReady;
	}

	public void Reset() {
		isAttackReady = true;
	}
}
