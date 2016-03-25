using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the tile Image on the HUD
/// </summary>
public class TileImageDisplay : MonoBehaviour {

    private Image tileImage;

    /// <summary>
    /// Initializes the tile image in the HUD
    /// </summary>
    private void Start() {
        tileImage = GetComponent<Image>();
    }

    /// <summary>
    /// Updates the tile image based on the selection in the HeadsUpDisplay
    /// </summary>
    private void Update() {
        tileImage.sprite = (HUDManager.getTile()) ? HUDManager.getTile().getDisplayImage() : null;
    }
}
