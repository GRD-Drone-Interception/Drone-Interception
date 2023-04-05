using System;
using DroneBehaviours.Scripts;
using DroneLoadout.Decorators;
using SavingSystem;
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
       
            // Load corresponding drone data file
            DroneData droneData = DroneSaveSystem.Load(droneConfigData.DroneName);
        
            // Instantiate attachment prefabs and position them at their attachment point positions specified in the droneData file 
            for (var i = 0; i < droneData.numAttachments; i++)
            {
                var path = droneData.attachmentDataPaths[i];
            
                GameObject droneAttachmentPrefab = Resources.Load<GameObject>(path);
                GameObject spawnedDroneAttachment = Instantiate(droneAttachmentPrefab);
                DroneAttachment droneAttachment = spawnedDroneAttachment.GetComponent<DroneAttachment>();
                drone.Decorate(droneAttachment, drone.GetAttachmentPoints()[droneData.mountedAttachmentPointIndex[i]]);
            }

            spawnedDrone.transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
            //drone.SetTeam(playerTeam); // get current team from Turnmanager
            DroneManager.AddDrone(drone);
            OnDroneSpawned?.Invoke(spawnedDrone);
            Debug.Log($"Drone Cost: {drone.DecorableDrone.Cost}");
        }
    }
}