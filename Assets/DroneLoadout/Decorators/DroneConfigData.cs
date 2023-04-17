using DroneLoadout.Factory;
using DroneLoadout.Scripts;
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
        public string DroneName;
        public GameObject DronePrefab;
        public GameObject DestructiblePrefab;
        public string DroneDescription;
        public DroneType DroneType;
        public string PrefabDataPath;
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
