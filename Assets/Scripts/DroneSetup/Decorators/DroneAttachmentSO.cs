using DroneSetup.Component;
using UnityEngine;

namespace DroneSetup.Decorators
{
    /// <summary>
    /// Used to configure the properties of an attachment through the
    /// inspector. 
    /// </summary>
    [CreateAssetMenu(fileName = "NewDroneAttachment", menuName = "Drone/Attachment", order = 1)]
    public class DroneAttachmentSO : ScriptableObject, IDrone
    {
        [SerializeField] private DroneAttachmentType droneAttachmentType;
        [SerializeField] private float cost;
        [SerializeField] private float range;
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float weight;

        public DroneAttachmentType DroneAttachmentType => droneAttachmentType;
        public string attachmentName;
        public GameObject attachmentPrefab;
        public string attachmentDescription;

        public float Cost => cost;
        public float Range => range;
        public float Speed => speed;
        public float Acceleration => acceleration;
        public float Weight => weight;
    }

    public enum DroneAttachmentType
    {
        Camera,
        Battery,
        Motor,
        Propeller,
        FlightController, // determine how the drone behaves
        Radar, // scanners, GPS
        Sensor, 
        Payload, // EMP and explosive charges?
        Weapon // turrets and missile launchers?
    }
}