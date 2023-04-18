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
        public string AttachmentName;
        public string Description;
        public DroneAttachmentType AttachmentType;
        public GameObject DronePrefab;
        public Sprite PrefabSprite;
        public string PrefabDataPath = "Attachments/";
        public List<DroneBehaviour> DroneBehaviours;
        public Color DecalColour;
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