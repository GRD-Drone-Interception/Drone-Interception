using System.Collections.Generic;
using Drone;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    public Transform DroneSpawnpoint => droneSpawnpoint;

    [SerializeField] private Transform droneSpawnpoint;
    [SerializeField] private List<InterceptorDrone> _drones = new();
    private bool _isEditing;

    public void AddToBench(InterceptorDrone drone)
    {
        _drones.Add(drone);
        //_drone = drone;
        //_isOccupied = true;
    }

    public void RemoveFromBench(InterceptorDrone drone)
    {
        _drones.Remove(drone);
        //_drone = null;
        //_isOccupied = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<InterceptorDrone>() != null)
        {
            Destroy(collision.transform.GetComponent<Rigidbody>());
            AddToBench(collision.transform.GetComponent<InterceptorDrone>());
        }
    }
}
