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

        private void OnEnable() => _button.onClick.AddListener(DecorateAttachmentPoint);
        private void OnDisable() => _button.onClick.RemoveListener(DecorateAttachmentPoint);
        private void Awake() => _button = GetComponent<Button>();

        private void DecorateAttachmentPoint() // TODO: Clean this
        {
            if (componentPrefab == null)
            {
                if (droneAttachmentSlot.GetAttachmentPoint().HasAttachment)
                {
                    var currentAttachment = droneAttachmentSlot.GetAttachmentPoint().GetDroneAttachment();
                    var currentAttachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
                    droneAttachmentSlot.GetDrone().RemoveAttachment(currentAttachment, currentAttachmentPoint);
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
        }
    }
}
