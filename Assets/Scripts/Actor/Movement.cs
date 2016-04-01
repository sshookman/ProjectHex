using UnityEngine;
using System.Collections;

/// <summary>
/// This class adds a movement component so the Actor can move around
/// the map. The range of movement is based on the maxSpeed and the
/// cost of the tiles being traversed.
/// </summary>
public class Movement : MonoBehaviour {

	[Tooltip("Determines the number of tiles that the actor can cross each turn")]
	[Range(5, 30)]
	public int maxSpeed;

	private int speed;

	/// <summary>
	/// Initializes the speed to the maxSpeed so the Actor can move
	/// </summary>
	private void Start () {
		speed = maxSpeed;
	}

	/// <summary>
	/// Getter for the speed that determines how far the
	/// Actor can move
	/// </summary>
	/// <returns>int - speed</returns>
	public int GetSpeed() {
		return speed;
	}

	/// <summary>
	/// Setter for the speed taht determinces how far the
	/// Actor can move
	/// </summary>
	/// <param name="speed">move speed int</param>
	public void SetSpeed(int speed) {
		this.speed = speed;
	}

	/// <summary>
	/// Check to see if the actor is Move Ready
	/// </summary>
	/// <returns>bool - Is Move Ready</returns>
	public bool IsReady() {
		return (speed > 0);
	}

	/// <summary>
	/// Resets the speed to maxSpeed
	/// </summary>
	public void Reset() {
		speed = maxSpeed;
	}
}
