using System.Collections.Generic;
using DroneLoadout;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    private static DroneManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError($"There should only be one instance of {this} in the scene!");
        }
    }

    public static List<Drone> ActiveDrones => _activeActiveDrones;
    private static List<Drone> _activeActiveDrones = new();

    public static void AddDrone(Drone drone)
    {
        _activeActiveDrones.Add(drone);
    }

    public static void RemoveDrone(Drone drone)
    {
        _activeActiveDrones.Remove(drone);
    }
}
