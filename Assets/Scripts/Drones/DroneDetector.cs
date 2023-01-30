using System;
using UnityEngine;

namespace Drones
{
    public class DroneDetector : MonoBehaviour
    {
        public static event Action<Drone> OnDroneDetected; 
        public static event Action OnDroneDetectionExit; 

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Drone>() != null)
            {
                OnDroneDetected?.Invoke(other.GetComponent<Drone>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Drone>() != null)
            {
                OnDroneDetectionExit?.Invoke();
            }
        }
    }
}
