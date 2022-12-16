using Drone;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    public Transform DroneSpawnpoint => droneSpawnpoint;

    [SerializeField] private Transform droneSpawnpoint;
    private IDrone _drone;
    private bool _isOccupied;

    private void OnEnable()
    {
        /*foreach (var droneCreator in FindObjectsOfType<DroneCreator>())
        {
            droneCreator.OnDroneCreated += AddToBench;
        }*/
    }

    public void AddToBench(IDrone drone)
    {
        _drone = drone;
        _isOccupied = true;
    }

    public void RemoveFromBench(IDrone drone)
    {
        _drone = null;
        _isOccupied = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<FixedWingDrone>() != null)
        {
            Destroy(collision.transform.GetComponent<Rigidbody>());
        }
    }
}
