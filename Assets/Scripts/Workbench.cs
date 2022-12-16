using System.Collections.Generic;
using Drone;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    public Transform DroneSpawnpoint => droneSpawnpoint;

    [SerializeField] private Transform droneSpawnpoint;
    //private IDrone _drone;
    [SerializeField] private List<FixedWingDrone> _drones = new();
    private FixedWingDrone _drone;
    private bool _isOccupied;

    private void OnEnable()
    {
        /*foreach (var droneCreator in FindObjectsOfType<DroneCreator>())
        {
            droneCreator.OnDroneCreated += AddToBench;
        }*/
    }

    public void AddToBench(FixedWingDrone drone)
    {
        _drones.Add(drone);
        //_drone = drone;
        //_isOccupied = true;
    }

    public void RemoveFromBench(FixedWingDrone drone)
    {
        _drones.Remove(drone);
        //_drone = null;
        //_isOccupied = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<FixedWingDrone>() != null)
        {
            Destroy(collision.transform.GetComponent<Rigidbody>());
            AddToBench(collision.transform.GetComponent<FixedWingDrone>());
        }
    }
}
