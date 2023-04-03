using System.Collections.Generic;
using DroneBehaviours.Scripts;
using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneLoadout.Decorators
{
    /// <summary>
    /// Used to configure the properties of an attachment through the
    /// inspector. 
    /// </summary>
    [CreateAssetMenu(fileName = "NewDroneAttachment", menuName = "Drone/Attachment", order = 1)]
    public class DroneAttachmentData : ScriptableObject, IDrone
    {
        public string AttachmentName => attachmentName;
        public DroneAttachmentType Type => type;
        public GameObject Prefab => prefab;
        public Sprite PrefabSprite => prefabSprite;
        public List<DroneBehaviour> DroneBehaviours => droneBehaviours;
        public Color DecalColour => decalColour;
        public float Cost => cost;
        public float Range => range;
        public float TopSpeed => speed;
        public float Acceleration => acceleration;
        public float Weight => weight;

        [SerializeField] private string attachmentName;
        [SerializeField] private string description;
        [SerializeField] private DroneAttachmentType type;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Sprite prefabSprite;
        [SerializeField] private float cost;
        [SerializeField] private float range;
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float weight;
        [SerializeField] private List<DroneBehaviour> droneBehaviours;
        [SerializeField] private Color decalColour;
    }
}