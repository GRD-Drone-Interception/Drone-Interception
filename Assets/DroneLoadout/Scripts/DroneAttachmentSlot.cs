using System;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout.Scripts
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
        private Image _image;
        private ColorBlock _highlightColourBlock;
        private ColorBlock _unhighlightColourBlock;

        private void OnEnable() => _button.onClick.AddListener(ToggleAttachmentSlotVisibilityOnSlotSelected);
        private void OnDisable() => _button.onClick.RemoveListener(ToggleAttachmentSlotVisibilityOnSlotSelected);
        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }
        private void Start()
        {
            componentSubMenuContainer.SetActive(false);
            
            _highlightColourBlock = new ColorBlock
            {
                normalColor = new Color(0, 1, 0.6f, 0.25f),
                highlightedColor = _button.colors.highlightedColor,
                pressedColor = _button.colors.pressedColor,
                selectedColor = new Color(0, 1, 0.6f, 0.25f),
                disabledColor = _button.colors.disabledColor,
                colorMultiplier = 1,
                fadeDuration = 0
            };
            
            _unhighlightColourBlock = new ColorBlock
            {
                normalColor = new Color(0, 0, 0, 0.8f),
                highlightedColor = _button.colors.highlightedColor,
                pressedColor = _button.colors.pressedColor,
                selectedColor = new Color(0, 0, 0, 0.8f),
                disabledColor = _button.colors.disabledColor,
                colorMultiplier = 1,
                fadeDuration = 0
            };
        }

        private void Update()
        {
            // TODO: Clean this
            if (_attachmentPoint.HasAttachment)
            {
                Highlight();
            }
            else
            {
                Unhighlight();
            }
        }

        private void ToggleAttachmentSlotVisibilityOnSlotSelected()
        {
            // If the component sub-menu isn't already displayed, display it
            if (!componentSubMenuContainer.activeSelf)
            {
                componentSubMenuContainer.SetActive(true);
                _image.fillCenter = false;
                _attachmentPoint.SetVisibility(true);
                if (_attachmentPoint.HasAttachment)
                {
                    _attachmentPoint.GetDroneAttachment().Pulsate(true);
                }
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
            _image.fillCenter = true;
            if (_attachmentPoint != null)
            {
                _attachmentPoint.SetVisibility(false);
                if (_attachmentPoint.HasAttachment)
                {
                    _attachmentPoint.GetDroneAttachment().Pulsate(false);
                    _attachmentPoint.GetDroneAttachment().ResetColour();
                }
            }
        }

        public void BindToDrone(Drone drone) => _drone = drone;
        public Drone GetDrone() => _drone;

        public void BindToAttachmentPoint(AttachmentPoint point) => _attachmentPoint = point;
        public AttachmentPoint GetAttachmentPoint() => _attachmentPoint;
        
        private void Highlight() => _button.colors = _highlightColourBlock;

        private void Unhighlight() => _button.colors = _unhighlightColourBlock;
    }
}
