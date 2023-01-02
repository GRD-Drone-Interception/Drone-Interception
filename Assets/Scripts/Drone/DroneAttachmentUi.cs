using System.Collections.Generic;
using Drone;
using UnityEngine;

public class DroneAttachmentUi : MonoBehaviour
{
    [SerializeField] private List<DroneOutfitter> attachmentDecorators;

    private void OnEnable() => DroneLoadoutCameraMode.OnModeChange += OnCameraModeChange;
    private void OnDisable() => DroneLoadoutCameraMode.OnModeChange -= OnCameraModeChange;

    private void Start()
    {
        //_attachmentDecorators.AddRange(FindObjectsOfType<AttachmentDecorator>());
        attachmentDecorators.ForEach(ctx => ctx.gameObject.SetActive(false));
    }

    private void OnCameraModeChange(DroneLoadoutCameraMode.CameraMode mode)
    {
        switch (mode)
        {
            case DroneLoadoutCameraMode.CameraMode.Edit:
            {
                attachmentDecorators.ForEach(ctx => ctx.gameObject.SetActive(true));
                break;
            }
            case DroneLoadoutCameraMode.CameraMode.Display:
            {
                attachmentDecorators.ForEach(ctx => ctx.gameObject.SetActive(false));
                break;
            }
        }
    }
}
