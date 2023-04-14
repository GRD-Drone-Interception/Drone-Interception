using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float moveSpeed = 10.0f; // speed of camera movement
    public float zoomSpeed = 10.0f; // speed of zoom

    private void Update()
    {
        float scroll = -1 * Input.GetAxis("Mouse ScrollWheel");

        // Move the camera up or down based on the mouse scroll
        transform.position += new Vector3(0, scroll * moveSpeed, 0);

        // Zoom in or out based on the mouse scroll
        Camera.main.fieldOfView -= scroll * zoomSpeed;

        // Limit the zoom range to prevent the camera from going too far away
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 10, 100);
    }
}
