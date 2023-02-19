using System;
using System.Collections.Generic;
using DroneWorkbench;
using UnityEngine;

namespace Drones
{
    public class DroneClassSpawnOnStart : MonoBehaviour
    {
        public static event Action OnDroneSpawned;
        
        [SerializeField] private List<GameObject> droneClassPrefabs;
        [SerializeField] private Workbench workbench;

        private void Start() => SpawnDroneClassOnStart();

        private void SpawnDroneClassOnStart()
        {
            var drone = Instantiate(droneClassPrefabs[0]);
            drone.transform.position = workbench.DroneSpawnPoint.position;
            workbench.AddToBench(drone.GetComponent<Drone>());
            OnDroneSpawned?.Invoke();
        }
    }
}