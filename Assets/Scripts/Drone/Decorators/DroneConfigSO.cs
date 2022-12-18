using Drone.Component;
using UnityEngine;

namespace Drone.Decorators
{
    [CreateAssetMenu(fileName = "NewDroneConfig", menuName = "Drone/Config", order = 1)]
    public class DroneConfigSO : ScriptableObject, IDrone
    {
        public DroneType droneType;
        
        [SerializeField] private float range;
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float weight;

        public string droneName;
        public GameObject dronePrefab;
        public string droneDescription;

        public float Range => range;
        public float Speed => speed;
        public float Acceleration => acceleration;
        public float Weight => weight;
    }
}
