using System;
using Drones;
using UnityEngine;
using UnityEngine.UI;

public class DroneAttachmentSlot : MonoBehaviour
{
    public event Action<DroneAttachmentSlot> OnAttachmentSlotSelected;
    
    [SerializeField] private GameObject attachmentSlotComponentsContainer;
    private AttachmentPoint _attachmentPoint;
    private Drone _drone;
    private Button _button;

    private void OnEnable() => _button.onClick.AddListener(OnDroneAttachmentSlotSelected);
    private void OnDisable() => _button.onClick.RemoveListener(OnDroneAttachmentSlotSelected);
    private void Awake() => _button = GetComponent<Button>();
    private void Start() => attachmentSlotComponentsContainer.SetActive(false);

    private void Update()
    {
        // TODO: Clean this - Handle in it's own class
        if (DroneLoadoutCameraMode.CurrentCameraMode == DroneLoadoutCameraMode.CameraMode.Edit)
        {
            if (_attachmentPoint != null)
            {
                if (!_attachmentPoint.IsVisible())
                {
                    return;
                }
                Camera.main.transform.LookAt(_attachmentPoint.transform);
            }
        }
    }

    private void OnDroneAttachmentSlotSelected()
    {
        // If the component sub-menu isn't already displayed, display it
        if (!attachmentSlotComponentsContainer.activeSelf)
        {
            attachmentSlotComponentsContainer.SetActive(true);
            _attachmentPoint.SetVisibility(true);
            //Camera.main.transform.LookAt(_attachmentPoint.transform);
            OnAttachmentSlotSelected?.Invoke(this);
        }
        else
        {
            HideComponentSubMenu();
        }
    }
    public void HideComponentSubMenu()
    {
        attachmentSlotComponentsContainer.SetActive(false);
        if (_attachmentPoint != null)
        {
            _attachmentPoint.SetVisibility(false);
        }
    }

    public void BindToDrone(Drone drone) => _drone = drone;
    public Drone GetDrone() => _drone;

    public void BindToAttachmentPoint(AttachmentPoint point) => _attachmentPoint = point;
    public AttachmentPoint GetAttachmentPoint() => _attachmentPoint;
}
