using Drones;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DroneWorkbench
{
    public class RotateWorkbenchDrones : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 100.0f;
        private Workbench _workbench;
        private DronePodiumInputActions _inputActions;
        private bool _rotateLeftIsPressed = false;
        private bool _rotateRightIsPressed = false;

        private void Awake()
        {
            _inputActions = new DronePodiumInputActions();
            _workbench = GetComponent<Workbench>();
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
            if(_workbench.DroneBeingEdited == null) { return; }

            if (DroneLoadoutCameraMode.CurrentCameraMode == DroneLoadoutCameraMode.CameraMode.Edit)
            {
                if (_rotateLeftIsPressed)
                {
                    _workbench.DroneBeingEdited.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                }

                if (_rotateRightIsPressed)
                {
                    _workbench.DroneBeingEdited.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
                }
            }
            else
            {
                _workbench.DroneBeingEdited.transform.Rotate(new Vector3(0,25.0f * Time.deltaTime,0));
            }
        }

        private void RotateLeftOnStarted(InputAction.CallbackContext obj) => _rotateLeftIsPressed = true;
        private void RotateLeftOnCanceled(InputAction.CallbackContext obj) => _rotateLeftIsPressed = false;
        private void RotateRightOnStarted(InputAction.CallbackContext obj) => _rotateRightIsPressed = true;
        private void RotateRightOnCanceled(InputAction.CallbackContext obj) => _rotateRightIsPressed = false;
    }
}