using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZoneCollider : MonoBehaviour
{
    [SerializeField] private List<DroneUnit> unitsInArea = new List<DroneUnit>();

    [SerializeField] private List<DroneUnit> team1InArea = new List<DroneUnit>();
    [SerializeField] private List<DroneUnit> team2InArea = new List<DroneUnit>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out DroneUnit drone))
        {
            Debug.Log("Drone entered collider");
            unitsInArea.Add(drone);

            if(drone.Team == PlayerTeam.Offensive)
            {
                team1InArea.Add(drone);
            }
            else if (drone.Team == PlayerTeam.Defensive)
            {
                team2InArea.Add(drone);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DroneUnit drone))
        {
            Debug.Log("Drone exited collider");
            unitsInArea.Remove(drone);

            if (drone.Team == PlayerTeam.Offensive)
            {
                team1InArea.Remove(drone);
            }
            else if (drone.Team == PlayerTeam.Defensive)
            {
                team2InArea.Remove(drone);
            }
        }
    }

    public List<DroneUnit> GetUnitsInArea()
    {
        return unitsInArea;
    }

    public List<DroneUnit> GetTeam1UnitsInArea()
    {
        return team1InArea;
    }

    public List<DroneUnit> GetTeam2UnitsInArea()
    {
        return team2InArea;
    }
}
