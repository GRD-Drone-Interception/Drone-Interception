using System;
using DroneBehaviours.Scripts;
using DroneLoadout.Decorators;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout.Scripts
{
    public class DroneSpawner : MonoBehaviour
    {
        public event Action<GameObject> OnDroneSpawned;

        [SerializeField] private DroneConfigData droneConfigData;
        private Button _spawnerButton;

        public void Awake() => _spawnerButton = GetComponent<Button>();
        private void OnEnable() => _spawnerButton.onClick.AddListener(SpawnDrone);
        private void OnDisable() => _spawnerButton.onClick.RemoveListener(SpawnDrone);

        private void SpawnDrone()
        {
            // Load default drone prefab from resources file
            GameObject dronePrefab = Resources.Load<GameObject>(droneConfigData.PrefabDataPath);
            GameObject spawnedDrone = Instantiate(dronePrefab);
            var drone = spawnedDrone.GetComponent<Drone>();
       
            // Assemble the drone data
            DroneSavedAttachmentsAssembler.BuildDrone(drone);

            spawnedDrone.transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
            //drone.SetTeam(playerTeam); // get current team from Turnmanager
            DroneManager.AddDrone(drone);
            OnDroneSpawned?.Invoke(spawnedDrone);
            Debug.Log($"Drone Cost: {drone.DecorableDrone.Cost}");
        }
    }
}