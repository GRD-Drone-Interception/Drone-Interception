using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for decorating an attachment point with a drone attachment via a button.
    /// </summary>
    public class DroneAttachmentOutfitter : MonoBehaviour
    {
        //public event Action<DroneAttachmentOutfitter> OnAttachmentSelected;
        
        [SerializeField] private GameObject componentPrefab;
        [SerializeField] private DroneAttachmentSlot droneAttachmentSlot;
        private Button _button;

        private void OnEnable() => _button.onClick.AddListener(DecorateAttachmentPoint);
        private void OnDisable() => _button.onClick.RemoveListener(DecorateAttachmentPoint);
        private void Awake() => _button = GetComponent<Button>();

        private void DecorateAttachmentPoint()
        {
            // I.e. Empty button
            if (componentPrefab == null)
            {
                if (droneAttachmentSlot.GetAttachmentPoint().HasAttachment)
                {
                    var currentAttachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
                    droneAttachmentSlot.GetDrone().RemoveAttachment(currentAttachmentPoint);
                    //OnAttachmentSelected?.Invoke(this);
                }
                return;
            }

            // If attachment point is empty, decorate it. Else, destroy the newly spawned component. 
            var droneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
            var attachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
            if (!droneAttachmentSlot.GetAttachmentPoint().HasAttachment)
            {
                droneAttachmentSlot.GetDrone().Decorate(droneAttachment, attachmentPoint);
                //OnAttachmentSelected?.Invoke(this);
            }
            else
            {
                Destroy(droneAttachment.gameObject);
            }
        }
    }
}
