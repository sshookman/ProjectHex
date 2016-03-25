using UnityEngine;

/// <summary>
/// This script allows the player to control the camera with the
/// arrow keys to pan and the mouse scroll wheel to zoom.
/// </summary>
public class CameraInputController : MonoBehaviour {

    [Tooltip("Controls how fast the camera will pan")]
    [Range(1, 10)]
    public int panSpeed;
    [Tooltip("Controls how fast the camera will zoom")]
    [Range(1, 10)]
    public int zoomSpeed;

    private Camera mainCamera;
	private Transform target;

    /// <summary>
    /// Initializes the Camera variable
    /// </summary>
    private void Start () {
        mainCamera = GetComponent<Camera>();
    }

	public void SetTarget(Transform target) {
		this.target = target;
	}

    /// <summary> 
    /// Handles the player input control for controlling the camera
    /// </summary>
    private void Update () {
        
		if (!target) {
			PanCamera();
	        ZoomCamera();
		} else {
			FollowTarget();
		}
    }

    /// <summary>
    /// Pans the camera based on arrow key input
    /// </summary>
    private void PanCamera() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(new Vector3(panSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(new Vector3(-panSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(new Vector3(0, -panSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(new Vector3(0, panSpeed * Time.deltaTime, 0));
        }
    }

    /// <summary>
    /// Zooms the camera based on the mouse scroll input
    /// </summary>
    private void ZoomCamera() {
        mainCamera.orthographicSize -= (Input.mouseScrollDelta.y * .10f * zoomSpeed);
    }

	private void FollowTarget() {
		Vector3 position = new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z);
		mainCamera.transform.position = position;
	}
}
