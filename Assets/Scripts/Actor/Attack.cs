using UnityEngine;
using System.Collections;

/// <summary>
/// This class adds an attack component for an Actor which allows the
/// Actor to attack another Actor once per turn.
/// </summary>
public class Attack : MonoBehaviour {

	private bool isAttackReady;

	/// <summary>
	/// Initializes the state so the Actor is attack ready
	/// </summary>
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

	/// <summary>
	/// Resets the attack readiness for the Actor
	/// </summary>
	public void Reset() {
		isAttackReady = true;
	}
}
