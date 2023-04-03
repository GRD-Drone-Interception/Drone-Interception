using System.Collections.Generic;
using DroneBehaviours.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace DroneLoadout.Decorators
{
    /// <summary>
    /// Used to configure the properties of an attachment through the
    /// inspector. 
    /// </summary>
    [CreateAssetMenu(fileName = "NewDroneAttachment", menuName = "Drone/Attachment", order = 1)]
    public class DroneAttachmentData : ScriptableObject, IDrone
    {
        public DroneAttachmentType Type => type;
        public List<DroneBehaviour> DroneBehaviours => droneBehaviours;
        public float Cost => cost;
        public float Range => range;
        public float TopSpeed => speed;
        public float Acceleration => acceleration;
        public float Weight => weight;

        public string attachmentName;
        [FormerlySerializedAs("attachmentDescription")] public string description;
        [FormerlySerializedAs("attachmentType")] [FormerlySerializedAs("droneAttachmentType")] [SerializeField] private DroneAttachmentType type;
        [FormerlySerializedAs("attachmentPrefab")] public GameObject prefab;
        public Sprite prefabSprite;
        [SerializeField] private float cost;
        [SerializeField] private float range;
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float weight;
        [SerializeField] private List<DroneBehaviour> droneBehaviours;
    }
}