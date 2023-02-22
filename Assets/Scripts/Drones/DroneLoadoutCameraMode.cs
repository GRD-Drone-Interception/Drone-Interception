using System;
using UnityEngine;
using UnityEngine.UI;

namespace Drones
{
    public class DroneLoadoutCameraMode : MonoBehaviour
    {
        public static event Action<CameraMode> OnModeChange;
    
        public enum CameraMode
        {
            Edit,
            Display
        }
        public static CameraMode CurrentCameraMode = CameraMode.Display;
    
        [SerializeField] private Button editButton;
        [SerializeField] private Button displayButton;
        private static Vector3 _cameraDisplayStartRotationEuler;

        private void Start() => _cameraDisplayStartRotationEuler = Camera.main.transform.rotation.eulerAngles;

        private void OnEnable()
        {
            editButton.onClick.AddListener(SetCameraModeToEdit);
            displayButton.onClick.AddListener(SetCameraModeToDisplay);
        }

        private void OnDisable()
        {
            editButton.onClick.RemoveListener(SetCameraModeToEdit);
            displayButton.onClick.RemoveListener(SetCameraModeToDisplay);
        }
    
        private static void SetCameraModeToEdit()
        {
            CurrentCameraMode = CameraMode.Edit;
            OnModeChange?.Invoke(CurrentCameraMode);
        }
    
        private static void SetCameraModeToDisplay()
        {
            CurrentCameraMode = CameraMode.Display;
            Camera.main.transform.eulerAngles = _cameraDisplayStartRotationEuler;
            OnModeChange?.Invoke(CurrentCameraMode);
        }
    }
}
