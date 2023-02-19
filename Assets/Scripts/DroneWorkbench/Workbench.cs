using System;
using System.Collections.Generic;
using Drones;
using UnityEngine;
using UnityEngine.UI;

namespace DroneWorkbench
{
    public class Workbench : MonoBehaviour
    {
        public event Action<Drone> OnDroneSpawned;
        public Drone DroneBeingEdited => _droneBeingEdited;

        [SerializeField] private List<GameObject> droneClassPrefabs;
        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Button resetDroneConfigButton;
        [SerializeField] private Button deleteDroneButton;
        //private Dictionary<GameObject, bool> _dronePrefabsDictionary = new();
        private Drone _droneBeingEdited;

        private void Awake()
        {
            SpawnDroneClassesOnStart();
        }

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
        
        private void Start()
        {
            resetDroneConfigButton.gameObject.SetActive(false);
        }

        public void AddToBench(Drone drone)
        {
            drone.transform.SetParent(transform);
            _droneBeingEdited = drone;
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
        }
        
        private void SpawnDroneClassesOnStart()
        {
            var droneGameObject = Instantiate(droneClassPrefabs[0]);
            droneGameObject.transform.position = droneSpawnPosition.position;
            
            var drone = droneGameObject.GetComponent<Drone>();
            AddToBench(drone);
            OnDroneSpawned?.Invoke(drone);
            /*_dronePrefabsDictionary.Add(drone, false);
            foreach (var dronePrefab in _dronePrefabsDictionary.Keys)
            {
                dronePrefab.gameObject.SetActive(_dronePrefabsDictionary[dronePrefab]);
            }*/
        }

        public void SpawnDronePrefab(GameObject prefab)
        {
            DeleteCurrentDrone();
            
            var droneGameObject = Instantiate(prefab);
            droneGameObject.transform.position = droneSpawnPosition.position;
            
            var drone = droneGameObject.GetComponent<Drone>();
            AddToBench(drone);
            OnDroneSpawned?.Invoke(drone);
        }

        /*public void SpawnDronePrefab(GameObject prefab)
        {
            _dronePrefabsDictionary[prefab] = true;
            foreach (var drone in _dronePrefabsDictionary.Keys)
            {
                drone.gameObject.SetActive(_dronePrefabsDictionary[drone]);
            }
        }

        public void DespawnDronePrefab(GameObject prefab)
        {
            _dronePrefabsDictionary[prefab] = false;
            foreach (var drone in _dronePrefabsDictionary.Keys)
            {
                drone.gameObject.SetActive(_dronePrefabsDictionary[drone]);
            }
        }*/
    }
}
