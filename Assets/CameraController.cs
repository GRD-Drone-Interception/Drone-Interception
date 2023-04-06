using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField]private float moveSpeed = 20f;
    [SerializeField]private float zoomSpeed = 100f;
    [SerializeField]private float rotateSpeed = 20f;
    [SerializeField]private float orbitSpeed = 20f;
    [SerializeField] private float minCameraHeight = 1f;
    
    //private bool isRotating = false;
    private bool _isOrbiting = false;
    private Vector3 _previousMousePosition;

    private void Update()
    {
        // Move camera
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        mainCamera.transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));

        // Zoom camera
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - zoomInput * zoomSpeed * Time.deltaTime, 5f, 90f);
        
        // Rotate camera
        if (Input.GetMouseButton(1))
        {
            float rotateInput = Input.GetAxis("Mouse X");
            Quaternion yRotation = Quaternion.Euler(0f, rotateInput * rotateSpeed * Time.deltaTime, 0f);
            mainCamera.transform.rotation = yRotation * mainCamera.transform.rotation;
        }
        
        // Orbit camera
        if (Input.GetMouseButtonDown(2))
        {
            _isOrbiting = true;
            _previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(2))
        {
            _isOrbiting = false;
        }
        if (_isOrbiting)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float deltaX = currentMousePosition.x - _previousMousePosition.x;
            float deltaY = currentMousePosition.y - _previousMousePosition.y;
            mainCamera.transform.RotateAround(mainCamera.transform.position, Vector3.up, deltaX * orbitSpeed * Time.deltaTime);
            mainCamera.transform.RotateAround(mainCamera.transform.position, -mainCamera.transform.right, deltaY * orbitSpeed * Time.deltaTime);

            _previousMousePosition = currentMousePosition;
        }
        
        // Clamp camera height
        float currentHeight = mainCamera.transform.position.y;
        if (currentHeight < minCameraHeight)
        {
            Vector3 newPosition = mainCamera.transform.position;
            newPosition.y = minCameraHeight;
            mainCamera.transform.position = newPosition;
        }
    }
}
