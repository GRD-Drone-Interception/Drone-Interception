using Drones.Decorators;
using UnityEngine;

namespace Drones
{
    public class DroneAttachment : MonoBehaviour
    {
        public DroneAttachmentSO AttachmentSo => droneAttachmentSo;
        [SerializeField] private DroneAttachmentSO droneAttachmentSo;
    }
}
