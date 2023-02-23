using DroneLoadout.Decorators;
using UnityEngine;
using UnityEngine.Serialization;

namespace DroneLoadout
{
    /// <summary>
    /// Attached to every drone attachment prefab, and should be added to any new ones that are made.
    /// </summary>
    public class DroneAttachment : MonoBehaviour
    {
        public DroneAttachmentData Data => droneData;
        [FormerlySerializedAs("droneAttachmentSo")] [SerializeField] private DroneAttachmentData droneData;
    }
}
