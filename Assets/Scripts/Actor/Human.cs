using UnityEngine;
using System;

/// <summary>
/// This script identifies a Human actor and contains the details 
/// on the Human's weapons and other equipment.
/// </summary>
public class Human : MonoBehaviour {
	
	[Tooltip("The maximum range for attacking with the human's weapon")]
	[Range(4, 40)]
	public int maxWeaponRange;
	[Tooltip("The minimum range for attacking with the human's weapon")]
	[Range(0, 20)]
	public int minWeaponRange;

	/// <summary>
	/// Getter for the max weapon range
	/// </summary>
	/// <returns>The max weapon range</returns>
	public int GetMaxWeaponRange() {
		return  maxWeaponRange;
	}

	/// <summary>
	/// Getter for the minimum weapon range
	/// </summary>
	/// <returns>The minimum weapon range</returns>
	public int GetMinWeaponRange() {
		return minWeaponRange;
	}
}
