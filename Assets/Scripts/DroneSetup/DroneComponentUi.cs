using System.Collections.Generic;
using UnityEngine;

namespace DroneSetup
{
    /// <summary>
    /// Responsible for hiding and displaying attachment slots and drone type
    /// buttons based on the drone loadout camera mode.
    /// </summary>
    public class DroneComponentUi : MonoBehaviour
    {
        private List<DroneAttachmentSlot> _attachmentSlots = new();
        private List<DroneTypeButton> _droneTypeButtons = new();

        private void Awake()
        {
            _attachmentSlots.AddRange(FindObjectsOfType<DroneAttachmentSlot>());
            _droneTypeButtons.AddRange(FindObjectsOfType<DroneTypeButton>());
        }

        private void OnEnable() => DroneLoadoutCameraMode.OnModeChange += OnDroneLoadoutCameraModeChange;
        private void OnDisable() => DroneLoadoutCameraMode.OnModeChange -= OnDroneLoadoutCameraModeChange;

        private void OnDroneLoadoutCameraModeChange(DroneLoadoutCameraMode.CameraMode mode)
        {
            switch (mode)
            {
                case DroneLoadoutCameraMode.CameraMode.Edit:
                    _attachmentSlots.ForEach(slot => slot.gameObject.SetActive(true));
                    _droneTypeButtons.ForEach(slot => slot.gameObject.SetActive(false));
                    break;
                case DroneLoadoutCameraMode.CameraMode.Display:
                    _attachmentSlots.ForEach(slot => slot.gameObject.SetActive(false));
                    _attachmentSlots.ForEach(slot => slot.HideComponentSubMenu());
                    _droneTypeButtons.ForEach(slot => slot.gameObject.SetActive(true));
                    break;
            }
        }
    }
}
