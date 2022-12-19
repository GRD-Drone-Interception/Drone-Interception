using System.Collections.Generic;
using Drone;
using UnityEngine;

public class DroneClassUi : MonoBehaviour // Combine this class with DroneAttachmentUi?
{
    [SerializeField] private List<DroneCreator> droneCreators;

    private void OnEnable() => DroneLoadoutCameraMode.OnModeChange += OnCameraModeChange;
    private void OnDisable() => DroneLoadoutCameraMode.OnModeChange -= OnCameraModeChange;

    private void OnCameraModeChange(DroneLoadoutCameraMode.CameraMode mode)
    {
        switch (mode)
        {
            case DroneLoadoutCameraMode.CameraMode.Edit:
            {
                droneCreators.ForEach(ctx => ctx.gameObject.SetActive(false));
                break;
            }
            case DroneLoadoutCameraMode.CameraMode.Display:
            {
                droneCreators.ForEach(ctx => ctx.gameObject.SetActive(true));
                break;
            }
        }
    }
}
