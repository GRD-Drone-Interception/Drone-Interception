using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] public  Transform followTransform;

    [Header("Movement")]
    [SerializeField] private float normalSpeed  = 1f;
    [SerializeField] private float fastSpeed    = 2f;
                     private float movementSpeed;
    [SerializeField] private float movementTime = 1f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 1f;

    [Header("Zoom")]
    [SerializeField] private Vector3 zoomSpeed = new Vector3(0, -10, 10);

    // transform variables
    private Vector3    newPostion;
    private Quaternion newRotation;
    private Vector3    newZoom;

    // mouse members
    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    [Header("Controls")]
    [SerializeField] private bool mouse    = true;
    [SerializeField] private bool keyboard = true;

    [Header("Camera Functionality")]
    [SerializeField] private bool movement = true;
    [SerializeField] private bool rotation = true;
    [SerializeField] private bool zoom     = true;

    private void Awake()
    {

    }

    void Start()
    {
        newPostion  = this.transform.position;
        newRotation = this.transform.rotation;
        newZoom     = cameraTransform.localPosition;
    }

    void Update()
    {
        if (followTransform != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, followTransform.position, Time.deltaTime * movementTime);
        }
        else
        {
            ManualCameraControl();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && followTransform != null)
        {
            Vector3 pos = followTransform.position;
            followTransform = null;
            newPostion = pos;
        }
    }

    private void ManualCameraControl()
    {
        if (mouse)
        {
            HandleMouseInput();
        }
        if (keyboard)
        {
            HandleKeyboardInput();
        }

        TransformCamera();
    }

    /// <summary>
    /// Handles mouse input using the Old Input Sytem to control camera
    /// </summary>
    private void HandleMouseInput()
    {
        // MOVEMENT
        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPostion = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        // ROTATION
        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 rotationDifference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-rotationDifference.x / 5f)); 
        }

        // ZOOM
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += 10f * Input.mouseScrollDelta.y * zoomSpeed;
        }
    }

    /// <summary>
    /// Handles keyboard input using the Old Input Sytem to control camera
    /// </summary>
    private void HandleKeyboardInput()
    {
        // SPEED
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        // FORWARD | BACKWARDS
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPostion += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPostion += (transform.forward * -movementSpeed);
        }

        // LEFT | RIGHT
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPostion += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPostion += (transform.right * movementSpeed);
        }

        //ROTATION
        if(Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);
        }

        //ZOOM
        if(Input.GetKey(KeyCode.R))
        {
            newZoom += zoomSpeed;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomSpeed;
        }
    }

    /// <summary>
    /// Uses the data from the HandleInput funcitons to transform the camera rig
    /// </summary>
    private void TransformCamera()
    {
        // TRANSFORM
        if(movement)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, newPostion, Time.deltaTime * movementTime);
        }
        if(rotation)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRotation, Time.deltaTime * movementTime);
        }
        if(zoom)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);

            // Clamp with set values
            /*Vector3 clampPos = cameraTransform.localPosition;

            clampPos.y = Mathf.Clamp(clampPos.y, 100.0f, 1000.0f);
            clampPos.z = Mathf.Clamp(clampPos.z, -1000.0f, -100.0f);

            cameraTransform.localPosition = clampPos;*/

            // Clamp with raycast value
            Ray ray = CameraRigManager.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 hitPos = hit.point;
                Vector3 clampPos = cameraTransform.localPosition;

                clampPos.y = Mathf.Clamp(clampPos.y, hitPos.y, 1500.0f);
                clampPos.z = Mathf.Clamp(clampPos.z, -1500.0f, -hitPos.y);

                cameraTransform.localPosition = clampPos;
            }
        }
    }
}
