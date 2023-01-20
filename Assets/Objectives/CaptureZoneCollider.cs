using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZoneCollider : MonoBehaviour
{
    [SerializeField] private List<DroneUnit> unitsInArea = new List<DroneUnit>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out DroneUnit drone))
        {
            Debug.Log("Drone entered collider");
            unitsInArea.Add(drone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DroneUnit drone))
        {
            Debug.Log("Drone exited collider");
            unitsInArea.Remove(drone);
        }
    }

    public List<DroneUnit> GetUnitsInArea()
    {
        return unitsInArea;
    }
}
