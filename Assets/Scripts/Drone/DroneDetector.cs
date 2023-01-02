using System;
using Drone;
using UnityEngine;

public class DroneDetector : MonoBehaviour
{
    public static event Action<InterceptorDrone> OnDroneDetected; 
    public static event Action OnDroneDetectionExit; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InterceptorDrone>() != null)
        {
            OnDroneDetected?.Invoke(other.GetComponent<InterceptorDrone>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InterceptorDrone>() != null)
        {
            OnDroneDetectionExit?.Invoke();
        }
    }
}
