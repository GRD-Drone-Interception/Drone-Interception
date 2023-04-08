using System;
using DroneBehaviours.Scripts;
using DroneLoadout.Decorators;
using Testing;
using UnityEngine;
using UnityEngine.UI;
using Utility;

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

        public void TestSpawn(string input)
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
                SpawnDrone();
            }
        }
        
        private void SpawnDrone()
        {
            // Load default drone prefab from resources file
            GameObject dronePrefab = Resources.Load<GameObject>(droneConfigData.PrefabDataPath);
            GameObject spawnedDrone = Instantiate(dronePrefab);
            var drone = spawnedDrone.GetComponent<Drone>();
       
            // Assemble the drone data
            if (JsonFileHandler.CheckFileExists(drone.DroneConfigData.DroneName))
            {
                DroneLoader.BuildDrone(drone);
            }

            spawnedDrone.transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
            drone.SetTeam(TurnManager.Instance.CurrentTeam); 
            DroneManager.AddDrone(drone);
            OnDroneSpawned?.Invoke(spawnedDrone);
            Debug.Log($"Drone Cost: {drone.DecorableDrone.Cost}");
        }
    }
}