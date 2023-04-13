using System;
using System.Collections.Generic;
using DroneBehaviours.Scripts;
using DroneLoadout.Decorators;
using DroneLoadout.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Testing
{
    public class DroneRaycastSpawner : MonoBehaviour
    {
        public event Action<GameObject> OnDroneSpawned;
    
        [SerializeField] private Camera tacticalCamera;
        private readonly List<DroneSpawner> _droneSpawners = new();
        private DroneSpawner _selectedDroneSpawner;

        private void Awake() => _droneSpawners.AddRange(FindObjectsOfType<DroneSpawner>());
        private void OnEnable() => _droneSpawners.ForEach(spawner => spawner.OnDroneSpawnerSelected += SetDroneSpawnerOnDroneSpawnerSelected);
        private void OnDisable() => _droneSpawners.ForEach(spawner => spawner.OnDroneSpawnerSelected -= SetDroneSpawnerOnDroneSpawnerSelected);

        private void Update()
        {
            // Check if the mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
        
            if (Input.GetMouseButtonDown(0) && _selectedDroneSpawner)
            {
                // Define a layer mask that only includes the spawnable layers
                LayerMask spawnableLayer = LayerMask.GetMask("Spawnable");

                // Cast a ray from the camera to the mouse position, using the spawnable layer mask
                Ray ray = tacticalCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, spawnableLayer))
                {
                    // Check if there are any colliders in the spawn area
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 1.75f);
                    bool canSpawnDrone = true;
                    foreach (Collider collider in colliders)
                    {
                        // Check if the collider is a drone
                        Drone drone = collider.GetComponent<Drone>();
                        if (drone != null)
                        {
                            canSpawnDrone = false;
                            break;
                        }
                    }
                    // Spawn the drone if there are no drones in the spawn area
                    if (canSpawnDrone)
                    {
                        // Instantiate the drone prefab at the hit point
                        SpawnDrone(_selectedDroneSpawner.DroneConfigData, hit.point + Vector3.up);
                    }
                }
            }
        }

        private void SetDroneSpawnerOnDroneSpawnerSelected(DroneSpawner droneSpawner)
        {
            _selectedDroneSpawner = droneSpawner;
        }

        private void SpawnDrone(DroneConfigData droneConfigData, Vector3 hitPoint)
        {
            // Load default drone prefab from resources file
            GameObject spawnedDrone = Instantiate(droneConfigData.DronePrefab);
            var drone = spawnedDrone.GetComponent<Drone>();

            // Assemble the drone data
            if (JsonFileHandler.CheckFileExists(drone.GetName()))
            {
                DroneAttachmentsLoader.Assemble(drone);
            }

            spawnedDrone.transform.SetPositionAndRotation(hitPoint, Quaternion.identity);
            drone.SetTeam(TurnManager.Instance.CurrentTeam); 
            DroneManager.AddDrone(drone);
            OnDroneSpawned?.Invoke(spawnedDrone);
        }
    
        /*public void TestSpawn(string input)
        {
            int numOfDrones = int.Parse(input);

            if (numOfDrones == 0 || numOfDrones > 20)
            {
                Debug.LogError("You are trying to spawn too few or too many drones at once!");
                return;
            }

            for (int i = 0; i < int.Parse(input); i++)
            {
                Debug.LogError("Spawn drone");
                //SpawnDrone();
            }
        }*/
    }
}
