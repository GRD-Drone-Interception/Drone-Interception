using System.Collections.Generic;
using Drone;
using UnityEngine;
using UnityEngine.UI;

public class Workbench : MonoBehaviour
{
    public InterceptorDrone DroneBeingEdited => _droneBeingEdited;
    public Transform DroneSpawnpoint => droneSpawnpoint;
    
    [SerializeField] private WorkbenchCarousel workbenchCarousel;
    [SerializeField] private Transform droneSpawnpoint;
    [SerializeField] private Button resetDroneConfigButton;
    private readonly List<InterceptorDrone> _drones = new();
    private InterceptorDrone _droneBeingEdited;

    private void OnEnable()
    {
        resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
        DroneDetector.OnDroneDetected += delegate(InterceptorDrone drone) { _droneBeingEdited = drone; };
        DroneDetector.OnDroneDetectionExit += delegate { _droneBeingEdited = null; };
    }

    private void OnDisable()
    {
        resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
        DroneDetector.OnDroneDetected -= delegate(InterceptorDrone drone) { _droneBeingEdited = drone; };
        DroneDetector.OnDroneDetectionExit -= delegate { _droneBeingEdited = null; };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<InterceptorDrone>() != null)
        {
            var drone = collision.transform.GetComponent<InterceptorDrone>();
            Destroy(collision.transform.GetComponent<Rigidbody>());
            AddToBench(drone);
        }
    }

    private void AddToBench(InterceptorDrone drone)
    {
        drone.transform.SetParent(workbenchCarousel.transform); 
        _drones.Add(drone);
    }

    private void RemoveFromBench(InterceptorDrone drone)
    {
        drone.transform.SetParent(null);
        _drones.Remove(drone);
    }
    
    private void ResetCurrentDroneConfig()
    {
        if (_droneBeingEdited != null)
        {
            _droneBeingEdited.ResetConfiguration();
        }
    }
}
