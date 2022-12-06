using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PodiumMovement : MonoBehaviour
{
    private DronePodiumInputActions _inputActions;
    private bool _rotateLeftIsPressed = false;
    private bool _rotateRightIsPressed = false;

    private void Awake()
    {
        _inputActions = new DronePodiumInputActions();
    }

    private void OnEnable()
    {
        _inputActions.DronePodium.RotateLeft.started += RotateLeftOnStarted;
        _inputActions.DronePodium.RotateRight.started += RotateRightOnStarted;
        _inputActions.DronePodium.RotateLeft.canceled += RotateLeftOnCanceled;
        _inputActions.DronePodium.RotateRight.canceled += RotateRightOnCanceled;
        _inputActions.Enable();
    }
    
    private void OnDisable()
    {
        _inputActions.DronePodium.RotateLeft.started -= RotateLeftOnStarted;
        _inputActions.DronePodium.RotateRight.started -= RotateRightOnStarted;
        _inputActions.DronePodium.RotateLeft.canceled -= RotateLeftOnCanceled;
        _inputActions.DronePodium.RotateRight.canceled -= RotateRightOnCanceled;
        _inputActions.Disable();
    }

    private void Update()
    {
        if (_rotateLeftIsPressed)
        {
            transform.Rotate(new Vector3(0,-1,0));
        }
        if (_rotateRightIsPressed)
        {
            transform.Rotate(new Vector3(0,1,0));
        }
    }

    private void RotateLeftOnStarted(InputAction.CallbackContext obj) => _rotateLeftIsPressed = true;
    private void RotateLeftOnCanceled(InputAction.CallbackContext obj) => _rotateLeftIsPressed = false;
    private void RotateRightOnStarted(InputAction.CallbackContext obj) => _rotateRightIsPressed = true;
    private void RotateRightOnCanceled(InputAction.CallbackContext obj) => _rotateRightIsPressed = false;
}
