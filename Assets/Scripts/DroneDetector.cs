using Drone;
using UnityEngine;

public class DroneDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InterceptorDrone>() != null)
        {
            DroneCarousel.Instance.DroneToBeEdited = other.GetComponent<InterceptorDrone>(); // TODO: Clean
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InterceptorDrone>() != null)
        {
            DroneCarousel.Instance.DroneToBeEdited = null;
        }
    }
}
