using Drones;
using UnityEngine;
using UnityEngine.UI;

public class DroneOutfitter2 : MonoBehaviour
{
    [SerializeField] private GameObject componentPrefab;
    [SerializeField] private DroneComponentButton droneComponentButton;
    private Button _button; 

    private void OnEnable() => _button.onClick.AddListener(DecorateAttachmentPoint);
    private void OnDisable() => _button.onClick.RemoveListener(DecorateAttachmentPoint);
    private void Awake() => _button = GetComponent<Button>();

    private void DecorateAttachmentPoint()
    {
        DroneAttachment droneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
        AttachmentPoint attachmentPoint = droneComponentButton.GetAttachmentPoint();
        droneComponentButton.GetDrone().Decorate(droneAttachment, attachmentPoint);
        //droneComponentButton.GetAttachmentPoint().AddAttachment();
    }
}
