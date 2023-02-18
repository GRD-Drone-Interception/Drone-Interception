using System;
using System.Collections.Generic;
using DroneWorkbench;
using UnityEngine;

namespace Drones
{
    public class DroneLoadoutSpawner : MonoBehaviour
    {
        public static event Action OnDroneSpawned;
        
        [SerializeField] private List<GameObject> droneClassPrefabs;
        [SerializeField] private Workbench workbench;

        private void Start() => SpawnDroneClasses();

        private void SpawnDroneClasses()
        {
            var drone = Instantiate(droneClassPrefabs[0]);
            drone.transform.position = workbench.DroneSpawnPoint.position;
            OnDroneSpawned?.Invoke();
            
            /*foreach (var droneClass in droneClassPrefabs)
            {
                var drone = Instantiate(droneClass);
                drone.transform.position = workbench.DroneSpawnPoint.position;
                //workbench.AddToBench(drone.GetComponent<Drone>(), carousel.Nodes[_currentDronePrefabIndex]);
                OnDroneSpawned?.Invoke();
            }*/
        }
    }
}