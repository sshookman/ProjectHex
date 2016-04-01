using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the unit details Text on the HUD
/// </summary>
public class UnitDetailsDisplay : MonoBehaviour {

    private Text unitDetails;

    /// <summary>
    /// Initializes the unit details in the HUD
    /// </summary>
    private void Start() {
        unitDetails = GetComponent<Text>();
        unitDetails.text = "";
    }

    /// <summary>
    /// Updates the unit details based on the selection in the HeadsUpDisplay
    /// </summary>
    private void Update() {
        unitDetails.text = (HUDManager.getActor()) ? HUDManager.getActor().getDisplayDetails() : "";
    }
}
