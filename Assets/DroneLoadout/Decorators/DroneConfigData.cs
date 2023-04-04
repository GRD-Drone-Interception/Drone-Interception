using DroneLoadout.Factory;
using DroneLoadout.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace DroneLoadout.Decorators
{
    /// <summary>
    /// Used to define the default configuration of drone units, which can be
    /// extended with decorators 
    /// </summary>
    [CreateAssetMenu(fileName = "NewDroneConfig", menuName = "Drone/Config", order = 1)]
    public class DroneConfigData : ScriptableObject, IDrone
    {
        [FormerlySerializedAs("droneName")] public string DroneName;
        [FormerlySerializedAs("dronePrefab")] public GameObject DronePrefab;
        [FormerlySerializedAs("droneDescription")] public string DroneDescription;
        [FormerlySerializedAs("droneType")] public DroneType DroneType;
        [FormerlySerializedAs("prefabDataPath")] public string DrefabDataPath;
        public float Cost => cost;
        public float Range => range;
        public float TopSpeed => topSpeed;
        public float Acceleration => acceleration;
        public float Weight => weight;
        
        [SerializeField] private float cost;
        [SerializeField] private float range;
        [SerializeField] private float topSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float weight;
    }
}
