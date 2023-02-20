using System;
using System.Collections.Generic;
using Drones;
using UnityEngine;
using UnityEngine.UI;

namespace DroneWorkbench
{
    public class Workbench : MonoBehaviour
    {
        public event Action<Drone> OnDroneBeingEditedChanged; 
        public Drone DroneBeingEdited => _droneBeingEdited;

        [SerializeField] private List<GameObject> droneClassPrefabs;
        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Button resetDroneConfigButton;
        [SerializeField] private Button deleteDroneButton;
        private Drone _droneBeingEdited;

        private void Awake() => SpawnDroneClassesOnStart();

        private void OnEnable()
        {
            resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
            deleteDroneButton.onClick.AddListener(DeleteCurrentDrone);
        }

        private void OnDisable()
        {
            resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
            deleteDroneButton.onClick.RemoveListener(DeleteCurrentDrone);
        }
        
        private void Start() => resetDroneConfigButton.gameObject.SetActive(false);

        public void AddToBench(Drone drone)
        {
            drone.transform.SetParent(transform);
            _droneBeingEdited = drone;
            OnDroneBeingEditedChanged?.Invoke(drone);
        }

        private void RemoveFromBench(Drone drone)
        {
            drone.transform.SetParent(null);
        }

        private void ResetCurrentDroneConfig()
        {
            if (_droneBeingEdited != null)
            {
                _droneBeingEdited.ResetConfiguration();
            }
        }
    
        private void DeleteCurrentDrone()
        {
            RemoveFromBench(_droneBeingEdited);
            Destroy(_droneBeingEdited.gameObject);
            OnDroneBeingEditedChanged?.Invoke(null);
        }
        
        private void SpawnDroneClassesOnStart()
        {
            var droneGameObject = Instantiate(droneClassPrefabs[0]);
            droneGameObject.transform.position = droneSpawnPosition.position;
            
            var drone = droneGameObject.GetComponent<Drone>();
            AddToBench(drone);
        }

        public void SpawnDronePrefab(GameObject prefab)
        {
            if (_droneBeingEdited != null)
            {
                DeleteCurrentDrone();
            }

            var droneGameObject = Instantiate(prefab);
            droneGameObject.transform.position = droneSpawnPosition.position;
            
            var drone = droneGameObject.GetComponent<Drone>();
            AddToBench(drone);
        }
    }
}
