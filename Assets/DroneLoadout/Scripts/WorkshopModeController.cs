using System;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for setting the drone loadout camera mode when
    /// the edit and exit buttons are pressed.
    /// </summary>
    public class WorkshopModeController : MonoBehaviour
    {
        public enum WorkshopMode
        {
            Edit,
            Display
        }
        
        public static event Action<WorkshopMode> OnModeChange;
        public static WorkshopMode currentWorkshopMode = WorkshopMode.Display;
        
        [SerializeField] private Button editButton;
        [SerializeField] private Button displayButton;

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
            OnModeChange?.Invoke(currentWorkshopMode);
        }
    }
}
