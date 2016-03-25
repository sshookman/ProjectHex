using UnityEngine;

/// <summary>
/// This Controller handles user input for the Map to allow State
/// transitions.
/// </summary>
public class MapInputController : MonoBehaviour {

    private Map map;

	/// <summary>
    /// Initializes the Map component variable
    /// </summary>
	private void Start () {
        map = GetComponent<Map>();
	}
	
    /// <summary>
    /// Handles input for the escape key to transition Map State
    /// back to Selection
    /// </summary>
	private void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            map.SetState(MapState.SELECTION);
        }
    }
}
