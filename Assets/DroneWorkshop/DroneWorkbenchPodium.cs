using DroneLoadout.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace DroneWorkshop
{
    public class DroneWorkbenchPodium : MonoBehaviour
    {
        [SerializeField] private float manualRotationSpeed = 100.0f;
        [SerializeField] private float autoRotationSpeed = 25.0f;
        private DronePodiumInputActions _inputActions;
        private DroneWorkbench _droneWorkbench;
        private bool _rotateLeftIsPressed;
        private bool _rotateRightIsPressed;

        private void Awake()
        {
            _inputActions = new DronePodiumInputActions();
            _droneWorkbench = GetComponent<DroneWorkbench>();
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
            if(_droneWorkbench.DroneOnBench == null) { return; }

            if (WorkshopModeController.currentWorkshopMode == WorkshopModeController.WorkshopMode.Edit)
            {
                if (_rotateLeftIsPressed)
                {
                    _droneWorkbench.DroneOnBench.transform.Rotate(new Vector3(0, -manualRotationSpeed * Time.deltaTime, 0));
                }

                if (_rotateRightIsPressed)
                {
                    _droneWorkbench.DroneOnBench.transform.Rotate(new Vector3(0, manualRotationSpeed * Time.deltaTime, 0));
                }
            }
            else
            {
                _droneWorkbench.DroneOnBench.transform.Rotate(new Vector3(0,autoRotationSpeed * Time.deltaTime,0));
            }
        }

        private void RotateLeftOnStarted(InputAction.CallbackContext obj) => _rotateLeftIsPressed = true;
        private void RotateLeftOnCanceled(InputAction.CallbackContext obj) => _rotateLeftIsPressed = false;
        private void RotateRightOnStarted(InputAction.CallbackContext obj) => _rotateRightIsPressed = true;
        private void RotateRightOnCanceled(InputAction.CallbackContext obj) => _rotateRightIsPressed = false;
    }
}