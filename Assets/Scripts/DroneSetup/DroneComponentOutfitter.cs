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
        
        /*private void Start()
        {
            pooledDroneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
        }*/

        private void DecorateAttachmentPoint()
        {
            DroneAttachment droneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
            AttachmentPoint attachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
            
            // TODO: Clean this
            // If attachment point empty, decorate it. Else, destroy the newly spawned component.
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
