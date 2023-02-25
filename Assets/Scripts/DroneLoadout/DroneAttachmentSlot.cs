using System;
using DroneLoadout.Decorators;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for displaying a list of compatible components from a given
    /// component sub-menu for an attachment point.
    /// </summary>
    public class DroneAttachmentSlot : MonoBehaviour
    {
        public event Action<DroneAttachmentSlot> OnAttachmentSlotSelected;
        public DroneAttachmentType AttachmentType => droneAttachmentType;

        [SerializeField] private DroneAttachmentType droneAttachmentType;
        [SerializeField] private GameObject componentSubMenuContainer;
        private AttachmentPoint _attachmentPoint;
        private Drone _drone;
        private Button _button;

        private void OnEnable() => _button.onClick.AddListener(ToggleAttachmentSlotVisibilityOnSlotSelected);
        private void OnDisable() => _button.onClick.RemoveListener(ToggleAttachmentSlotVisibilityOnSlotSelected);
        private void Awake() => _button = GetComponent<Button>();
        private void Start() => componentSubMenuContainer.SetActive(false);

        private void ToggleAttachmentSlotVisibilityOnSlotSelected()
        {
            // If the component sub-menu isn't already displayed, display it
            if (!componentSubMenuContainer.activeSelf)
            {
                componentSubMenuContainer.SetActive(true);
                _attachmentPoint.SetVisibility(true);
                OnAttachmentSlotSelected?.Invoke(this);
            }
            else
            {
                HideComponentSubMenu();
            }
        }
        public void HideComponentSubMenu()
        {
            componentSubMenuContainer.SetActive(false);
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
}
