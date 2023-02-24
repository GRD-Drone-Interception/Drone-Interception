using System;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for decorating an attachment point with a drone attachment through a button.
    /// </summary>
    public class DroneComponentOutfitter : MonoBehaviour
    {
        [SerializeField] private GameObject componentPrefab;
        [SerializeField] private DroneAttachmentSlot droneAttachmentSlot;
        private Button _button;
        /*private ColorBlock _highlightColourBlock;
        private ColorBlock _startColourBlock;*/

        private void OnEnable() => _button.onClick.AddListener(DecorateAttachmentPoint);
        private void OnDisable() => _button.onClick.RemoveListener(DecorateAttachmentPoint);
        private void Awake() => _button = GetComponent<Button>();

        /*private void Start()
        {
            _highlightColourBlock = new ColorBlock
            {
                normalColor = new Color(0, 1, 0.67f, 0.02f),
                highlightedColor = _button.colors.highlightedColor,
                pressedColor = _button.colors.pressedColor,
                selectedColor = new Color(0, 1, 0.67f, 0.02f),
                disabledColor = _button.colors.disabledColor,
                colorMultiplier = _button.colors.colorMultiplier,
                fadeDuration = _button.colors.fadeDuration
            };
            _startColourBlock = new ColorBlock()
            {
                normalColor = new Color(0, 0, 0, 0.8f),
                highlightedColor = _button.colors.highlightedColor,
                pressedColor = _button.colors.pressedColor,
                selectedColor = new Color(0, 0, 0, 0.8f),
                disabledColor = _button.colors.disabledColor,
                colorMultiplier = _button.colors.colorMultiplier,
                fadeDuration = _button.colors.fadeDuration
            };
        }*/

        /*private void Update()
        {
            if (droneAttachmentSlot.GetAttachmentPoint().HasAttachment)
            {
                _button.colors = _highlightColourBlock;
            }
            else
            {
                _button.colors = _startColourBlock;
            }
        }*/

        private void DecorateAttachmentPoint() 
        {
            if (componentPrefab == null)
            {
                if (droneAttachmentSlot.GetAttachmentPoint().HasAttachment)
                {
                    var currentAttachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
                    droneAttachmentSlot.GetDrone().RemoveAttachment(currentAttachmentPoint);
                }
                return;
            }

            // If attachment point is empty, decorate it. Else, destroy the newly spawned component. 
            var droneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
            var attachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
            if (!droneAttachmentSlot.GetAttachmentPoint().HasAttachment)
            {
                droneAttachmentSlot.GetDrone().Decorate(droneAttachment, attachmentPoint);
            }
            else
            {
                Destroy(droneAttachment.gameObject);
            }
            
            
            
            /*var attachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
            
            if (attachmentPoint.HasAttachment)
            {
                // Remove the existing attachment so a new one can replace it
                droneAttachmentSlot.GetDrone().RemoveAttachment(attachmentPoint);
                
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = new Color(0, 0, 0, 0.8f);
                colorBlock.highlightedColor = _button.colors.highlightedColor;
                colorBlock.pressedColor = _button.colors.pressedColor;
                colorBlock.selectedColor = new Color(0, 0, 0, 0.8f);
                colorBlock.disabledColor = _button.colors.disabledColor;
                colorBlock.colorMultiplier = _button.colors.colorMultiplier;
                colorBlock.fadeDuration = _button.colors.fadeDuration;
                _button.colors = colorBlock;
                
                return;
            }

            // Attach the new component
            var newDroneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
            droneAttachmentSlot.GetDrone().Decorate(newDroneAttachment, attachmentPoint);
            
            ColorBlock colorBlock2 = new ColorBlock();
            colorBlock2.normalColor = new Color(0, 1, 0.67f, 0.02f);
            colorBlock2.highlightedColor = _button.colors.highlightedColor;
            colorBlock2.pressedColor = _button.colors.pressedColor;
            colorBlock2.selectedColor = new Color(0, 1, 0.67f, 0.02f);
            colorBlock2.disabledColor = _button.colors.disabledColor;
            colorBlock2.colorMultiplier = _button.colors.colorMultiplier;
            colorBlock2.fadeDuration = _button.colors.fadeDuration;
            _button.colors = colorBlock2;*/
            
            
            
            
            /*var attachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
            
            if (attachmentPoint.HasAttachment)
            {
                // Remove the existing attachment so a new one can replace it
                droneAttachmentSlot.GetDrone().RemoveAttachment(attachmentPoint);
                
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = new Color(0, 0, 0, 0.8f);
                colorBlock.highlightedColor = _button.colors.highlightedColor;
                colorBlock.pressedColor = _button.colors.pressedColor;
                colorBlock.selectedColor = new Color(0, 0, 0, 0.8f);
                colorBlock.disabledColor = _button.colors.disabledColor;
                colorBlock.colorMultiplier = _button.colors.colorMultiplier;
                colorBlock.fadeDuration = _button.colors.fadeDuration;
                _button.colors = colorBlock;
                
                return;
            }

            if (componentPrefab != null)
            {
                // Attach the new component
                var newDroneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
                droneAttachmentSlot.GetDrone().Decorate(newDroneAttachment, attachmentPoint);
                
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = new Color(0, 1, 0.67f, 0.02f);
                colorBlock.highlightedColor = _button.colors.highlightedColor;
                colorBlock.pressedColor = _button.colors.pressedColor;
                colorBlock.selectedColor = new Color(0, 1, 0.67f, 0.02f);
                colorBlock.disabledColor = _button.colors.disabledColor;
                colorBlock.colorMultiplier = _button.colors.colorMultiplier;
                colorBlock.fadeDuration = _button.colors.fadeDuration;
                _button.colors = colorBlock;
            }*/
        }
    }
}
