using UnityEngine;
using UnityEngine.UI;

namespace DroneSetup
{
    /// <summary>
    /// Responsible for decorating an attachment point with a drone attachment.
    /// </summary>
    public class DroneComponentOutfitter : MonoBehaviour
    {
        [SerializeField] private GameObject componentPrefab;
        [SerializeField] private DroneAttachmentSlot droneAttachmentSlot;
        private Button _button; 

        private void OnEnable() => _button.onClick.AddListener(DecorateAttachmentPoint);
        private void OnDisable() => _button.onClick.RemoveListener(DecorateAttachmentPoint);
        private void Awake() => _button = GetComponent<Button>();

        private void DecorateAttachmentPoint()
        {
            if (!droneAttachmentSlot.GetDrone().GetAttachmentPoints()[0].HasAttachment) // or if attachment is a different component
            {
                DroneAttachment droneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
                AttachmentPoint attachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
                droneAttachmentSlot.GetDrone().Decorate(droneAttachment, attachmentPoint);
            }
        }
    }
}
