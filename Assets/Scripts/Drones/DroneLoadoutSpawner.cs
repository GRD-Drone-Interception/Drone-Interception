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
        private int _currentDronePrefabIndex;

        private void Start() => SpawnDroneClasses();

        private void SpawnDroneClasses()
        {
            foreach (var droneClass in droneClassPrefabs)
            {
                var carousel = workbench.Carousel;
                if (_currentDronePrefabIndex >= carousel.Nodes.Count)
                {
                    Debug.LogWarning("Drone loadout prefabs exceeds the number of nodes in the scene. Excess drone classes will not be spawned.", this);
                    return;
                }
                var drone = Instantiate(droneClass);
                drone.transform.position = carousel.Nodes[_currentDronePrefabIndex].transform.position;
                workbench.AddToBench(drone.GetComponent<Drone>(), carousel.Nodes[_currentDronePrefabIndex]);
                _currentDronePrefabIndex++;
                OnDroneSpawned?.Invoke();
            }
        }
    }
}