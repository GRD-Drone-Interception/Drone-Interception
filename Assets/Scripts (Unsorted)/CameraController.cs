using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField]private float moveSpeed = 20f;
    [SerializeField]private float rotateSpeed = 20f;
    [SerializeField]private float scrollSpeed = 1000f;
    [SerializeField]private float orbitSpeed = 20f;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float minCameraHeight = 1f;
    [SerializeField] private float shiftMoveSpeedMultiplier = 2f;
    private Vector3 _moveVelocity = Vector3.zero;
    private Vector3 _previousMousePosition;
    private bool _isOrbiting;

    private void Update()
    {
        // Move camera
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized; // Normalize to avoid diagonal movement
        moveDirection = mainCamera.transform.TransformDirection(moveDirection); // Convert to world space direction
        moveDirection.y = 0f; // Ignore Y axis
        float currentMoveSpeed = moveSpeed; 
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
        {
            currentMoveSpeed *= shiftMoveSpeedMultiplier; 
        }
        Vector3 targetPosition = mainCamera.transform.position + moveDirection * (currentMoveSpeed * Time.deltaTime);
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref _moveVelocity, smoothTime);

        // Move camera closer/further
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 newScrollPosition = mainCamera.transform.position + mainCamera.transform.forward * (scrollInput * scrollSpeed * Time.deltaTime);
        mainCamera.transform.position = newScrollPosition;
        
        // Move camera up/down
        float upInput = Input.GetKey(KeyCode.Q) ? 1f : 0f;
        float downInput = Input.GetKey(KeyCode.E) ? 1f : 0f;
        Vector3 verticalDirection = new Vector3(0f, upInput - downInput, 0f).normalized;
        Vector3 targetYPosition = mainCamera.transform.position + verticalDirection * (moveSpeed * Time.deltaTime);
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetYPosition, ref _moveVelocity, smoothTime);

        // Rotate camera
        if (Input.GetMouseButton(1))
        {
            float rotateInput = Input.GetAxis("Mouse X");
            Quaternion targetRotation = Quaternion.Euler(0f, rotateInput * rotateSpeed * Time.deltaTime, 0f) * mainCamera.transform.rotation;
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, smoothTime);
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
