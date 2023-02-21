using System;
using Drones;
using UnityEngine;
using UnityEngine.UI;

public class DroneComponentButton : MonoBehaviour
{
    public event Action<DroneComponentButton> OnDroneComponentSelected;
    
    [SerializeField] private GameObject droneComponentSubMenuContainer;
    private AttachmentPoint _attachmentPoint;
    private Drone _drone;
    private Button _button; 

    private void OnEnable() => _button.onClick.AddListener(ShowSubMenu);
    private void OnDisable() => _button.onClick.RemoveListener(ShowSubMenu);
    private void Awake() => _button = GetComponent<Button>();
    private void Start() => droneComponentSubMenuContainer.SetActive(false);

    private void ShowSubMenu()
    {
        droneComponentSubMenuContainer.SetActive(true);
        _attachmentPoint.SetVisibility(true);
        OnDroneComponentSelected?.Invoke(this);
    }

    public void HideSubMenu() => droneComponentSubMenuContainer.SetActive(false);

    public void BindToDrone(Drone drone)
    {
        _drone = drone;
    }

    public Drone GetDrone()
    {
        return _drone;
    }

    public void BindToAttachmentPoint(AttachmentPoint point)
    {
        _attachmentPoint = point;
    }

    public AttachmentPoint GetAttachmentPoint()
    {
        return _attachmentPoint;
    }
}
