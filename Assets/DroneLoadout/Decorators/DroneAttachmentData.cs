using System.Collections.Generic;
using DroneBehaviours.Scripts;
using DroneLoadout.Scripts;
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
        [FormerlySerializedAs("attachmentName")] public string AttachmentName;
        [FormerlySerializedAs("description")] public string Description;
        [FormerlySerializedAs("attachmentType")] [FormerlySerializedAs("type")] public DroneAttachmentType AttachmentType;
        [FormerlySerializedAs("Prefab")] [FormerlySerializedAs("prefab")] public GameObject DronePrefab;
        [FormerlySerializedAs("prefabSprite")] public Sprite PrefabSprite;
        [FormerlySerializedAs("prefabDataPath")] public string PrefabDataPath;
        [FormerlySerializedAs("droneBehaviours")] public List<DroneBehaviour> DroneBehaviours;
        public float Cost => cost;
        public float Range => range;
        public float TopSpeed => speed;
        public float Acceleration => acceleration;
        public float Weight => weight;
        
        [SerializeField] private float cost;
        [SerializeField] private float range;
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float weight;
    }
}