using Drones.Component;
using UnityEngine;

namespace Drones.Decorators
{
    [CreateAssetMenu(fileName = "NewDroneAttachment", menuName = "Drone/Attachment", order = 1)]
    public class DroneAttachmentSO : ScriptableObject, IDrone
    {
        [SerializeField] private float range;
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float weight;

        public string attachmentName;
        public GameObject attachmentPrefab;
        public string attachmentDescription;

        public float Range => range;
        public float Speed => speed;
        public float Acceleration => acceleration;
        public float Weight => weight;
    }
}