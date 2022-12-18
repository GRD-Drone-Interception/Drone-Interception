using Drone.Decorators;
using UnityEngine;

public class AttachmentMonobehaviour : MonoBehaviour
{
    public DroneAttachmentSO AttachmentSo => droneAttachmentSo;
    [SerializeField] private DroneAttachmentSO droneAttachmentSo;
}
