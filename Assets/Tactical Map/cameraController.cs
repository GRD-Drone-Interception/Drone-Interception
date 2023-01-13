using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform cameraTransform;

    [Header("Movement")]
    [SerializeField] private float normalSpeed  = 1f;
    [SerializeField] private float fastSpeed    = 2f;
                     private float movementSpeed;
    [SerializeField] private float movementTime = 1f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 1f;

    [Header("Zoom")]
    [SerializeField] private Vector3 zoomSpeed = new Vector3(0, -10, 10);

    private Vector3    newPostion;
    private Quaternion newRotation;
    private Vector3    newZoom;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    [Header("Controls")]
    [SerializeField] private bool mouse    = true;
    [SerializeField] private bool keyboard = true;

    void Start()
    {
        newPostion  = this.transform.position;
        newRotation = this.transform.rotation;
        newZoom     = cameraTransform.localPosition;
    }

    void Update()
    {
        if(mouse)
        {
            HandleMouseInput();
        }
        if(keyboard)
        {
            HandleKeyboardInput();
        }

        TransformCamera();
    }

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
            newZoom += Input.mouseScrollDelta.y * zoomSpeed * 10f;
        }
    }

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

    private void TransformCamera()
    {
        // TRANSFORM
        this.transform.position = Vector3.Lerp(this.transform.position, newPostion, Time.deltaTime * movementTime);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
