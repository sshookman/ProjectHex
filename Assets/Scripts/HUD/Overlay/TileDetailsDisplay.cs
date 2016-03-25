using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the tile details Text on the HUD
/// </summary>
public class TileDetailsDisplay : MonoBehaviour {

    private Text tileDetails;

    /// <summary>
    /// Initializes the tile details in the HUD
    /// </summary>
    private void Start() {
        tileDetails = GetComponent<Text>();
        tileDetails.text = "";
    }

    /// <summary>
    /// Updates the tile details based on the selection in the HeadsUpDisplay
    /// </summary>
    private void Update() {
        tileDetails.text = (HUDManager.getTile()) ? HUDManager.getTile().getDisplayDetails() : "";
    }
}