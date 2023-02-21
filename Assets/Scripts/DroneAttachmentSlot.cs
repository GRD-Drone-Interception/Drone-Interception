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

    private void OnEnable() => _button.onClick.AddListener(ShowComponentSubMenu);
    private void OnDisable() => _button.onClick.RemoveListener(ShowComponentSubMenu);
    private void Awake() => _button = GetComponent<Button>();
    private void Start() => attachmentSlotComponentsContainer.SetActive(false);

    private void ShowComponentSubMenu()
    {
        // If the component sub-menu isn't already displayed, display it
        if (!attachmentSlotComponentsContainer.activeSelf)
        {
            attachmentSlotComponentsContainer.SetActive(true);
            _attachmentPoint.SetVisibility(true);
            OnAttachmentSlotSelected?.Invoke(this);
        }
        else
        {
            HideComponentSubMenu();
        }
    }
    public void HideComponentSubMenu() => attachmentSlotComponentsContainer.SetActive(false);

    public void BindToDrone(Drone drone) => _drone = drone;
    public Drone GetDrone() => _drone;

    public void BindToAttachmentPoint(AttachmentPoint point) => _attachmentPoint = point;
    public AttachmentPoint GetAttachmentPoint() => _attachmentPoint;
}
