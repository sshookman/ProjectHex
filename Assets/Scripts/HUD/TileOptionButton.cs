using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to initialize the buttons on tile options
/// so that they can set the corresponding mode on the Hex Map
/// when clicked;
/// </summary>
public class TileOptionButton : MonoBehaviour {

    [Tooltip("The Map State that will be set upon clicking the button")]
    public MapState newstate;

    /// <summary>
    /// Initializes the onClick method for the button to set the
    /// new map state and enables the button if the Actor's
    /// conditions are met
    /// </summary>
    public void Initialize(Actor actor) {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            Map map = GetComponentInParent<Map>();
            map.SetState(newstate);
        });

        switch (newstate) {
            case MapState.MOVEMENT:
				if (actor.GetMovement() && actor.GetMovement().IsReady()) {
                    EnableButton();
                }
                return;
            case MapState.ATTACK:
				if (actor.GetAttack() && actor.GetAttack().IsReady()) {
                    EnableButton();
                }
                return;
            case MapState.SKILL:
				if (actor.GetSkills() && actor.GetSkills().IsReady()) {
                    EnableButton();
                }
                return;
            default:
                return;
        }
    }

    /// <summary>
    /// Enables the button and image on the GameObject
    /// </summary>
    private void EnableButton() {
        GetComponent<Button>().enabled = true;
        GetComponent<Image>().enabled = true;
    }
}
