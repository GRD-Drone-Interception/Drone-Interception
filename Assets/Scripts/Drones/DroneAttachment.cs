using Drones.Decorators;
using UnityEngine;

namespace Drones
{
    /// <summary>
    /// Attached to every drone attachment prefab, and should be added to any new ones that are made.
    /// </summary>
    public class DroneAttachment : MonoBehaviour
    {
        public DroneAttachmentSO AttachmentSo => droneAttachmentSo;
        [SerializeField] private DroneAttachmentSO droneAttachmentSo;
    }
}
