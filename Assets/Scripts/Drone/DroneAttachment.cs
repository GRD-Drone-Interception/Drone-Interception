using Drone.Decorators;
using UnityEngine;

public class DroneAttachment : MonoBehaviour
{
    public DroneAttachmentSO AttachmentSo => droneAttachmentSo;
    [SerializeField] private DroneAttachmentSO droneAttachmentSo;
}
