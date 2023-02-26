using System;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    public enum WorkshopMode
    {
        Edit,
        Display
    }
    /// <summary>
    /// Responsible for setting the drone loadout camera mode when
    /// the edit and exit buttons are pressed.
    /// </summary>
    public class WorkshopModeController : MonoBehaviour
    {
        public static event Action<WorkshopMode> OnModeChange;
        public static WorkshopMode currentWorkshopMode = WorkshopMode.Display;
    
        [SerializeField] private Camera displayCamera;
        [SerializeField] private Camera focusCamera;
        [SerializeField] private Button editButton;
        [SerializeField] private Button displayButton;
        //private Vector3 _cameraDisplayStartRotationEuler;
        //private Vector3 _focusCameraStartRotationEuler;

        private void Start()
        {
            //_cameraDisplayStartRotationEuler = displayCamera.transform.rotation.eulerAngles;
            //_focusCameraStartRotationEuler = focusCamera.transform.rotation.eulerAngles;
        }

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
    
        private void SetCameraModeToEdit()
        {
            currentWorkshopMode = WorkshopMode.Edit;
            OnModeChange?.Invoke(currentWorkshopMode);
        }
    
        private void SetCameraModeToDisplay()
        {
            currentWorkshopMode = WorkshopMode.Display;
            //displayCamera.transform.eulerAngles = _cameraDisplayStartRotationEuler;
            //focusCamera.transform.eulerAngles = _focusCameraStartRotationEuler;
            OnModeChange?.Invoke(currentWorkshopMode);
        }
    }
}
