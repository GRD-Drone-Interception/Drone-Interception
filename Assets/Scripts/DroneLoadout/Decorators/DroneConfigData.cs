using DroneLoadout.Component;
using DroneLoadout.Factory;
using UnityEngine;

namespace DroneLoadout.Decorators
{
    /// <summary>
    /// Used to define the default configuration of drone units, which can be
    /// extended with decorators 
    /// </summary>
    [CreateAssetMenu(fileName = "NewDroneConfig", menuName = "Drone/Config", order = 1)]
    public class DroneConfigData : ScriptableObject, IDrone
    {
        public DroneType droneType;
        
        [SerializeField] private float cost;
        [SerializeField] private float range;
        [SerializeField] private float topSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float weight;

        public string droneName;
        public GameObject dronePrefab;
        public string droneDescription;

        public float Cost => cost;
        public float Range => range;
        public float TopSpeed => topSpeed;
        public float Acceleration => acceleration;
        public float Weight => weight;
    }
}
