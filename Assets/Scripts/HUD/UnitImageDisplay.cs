using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the unit Image on the HUD
/// </summary>
public class UnitImageDisplay : MonoBehaviour {

    private Image unitImage;

    /// <summary>
    /// Initializes the unit image in the HUD
    /// </summary>
	private void Start () {
        unitImage = GetComponent<Image>();
	}

    /// <summary>
    /// Updates the unit image based on the selection in the HeadsUpDisplay
    /// </summary>
    private void Update () {
        unitImage.sprite = (HUDManager.getActor()) ? HUDManager.getActor().getDisplayImage() : null;
	}
}
