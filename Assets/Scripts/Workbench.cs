using System;
using System.Collections.Generic;
using Drone;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    public Transform DroneSpawnpoint => droneSpawnpoint;
    [SerializeField] private Transform droneSpawnpoint;
    private List<InterceptorDrone> _drones = new();
    private InterceptorDrone _editedDrone;
    private int _editedDroneIndex;

    private void Update()
    {
        if (_editedDrone != null)
        {
            _editedDrone = _drones[_editedDroneIndex];
        }
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
        drone.transform.SetParent(DroneCarousel.Instance.transform); // TODO: Cleaner method preferred
        _drones.Add(drone);
    }

    private void RemoveFromBench(InterceptorDrone drone)
    {
        drone.transform.SetParent(null);
        _drones.Remove(drone);
    }
}
