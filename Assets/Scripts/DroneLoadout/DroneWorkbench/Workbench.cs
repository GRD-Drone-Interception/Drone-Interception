using System;
using System.IO;
using Testing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout.DroneWorkbench
{
    /// <summary>
    /// Responsible for adding, removing, decorating, and resetting drone
    /// objects that have been added to the workbench. 
    /// </summary>
    public class Workbench : MonoBehaviour
    {
        public event Action<Drone> OnDroneOnBenchChanged; 
        public Drone DroneOnBench => _droneOnBench;
        
        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Button addToFleetButton;
        [SerializeField] private Button editDroneButton;
        [SerializeField] private Button resetDroneConfigButton;
        private Drone _droneOnBench;
        private Player _player;

        private void Awake() => _player = FindObjectOfType<Player>();
        private void OnEnable()
        {
            addToFleetButton.onClick.AddListener(AddDroneToFleet);
            resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
        }

        private void OnDisable()
        {
            addToFleetButton.onClick.RemoveListener(AddDroneToFleet);
            resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
        }

        private void Start() => resetDroneConfigButton.gameObject.SetActive(false);

        private void Update()
        {
            // TODO: Call this on an event trigger. This shouldn't be in Update
            if (DroneLoadoutCameraMode.CurrentCameraMode == DroneLoadoutCameraMode.CameraMode.Display)
            {
                addToFleetButton.gameObject.SetActive(_droneOnBench != null);
                editDroneButton.gameObject.SetActive(_droneOnBench != null);
            }
        }

        private void AddToBench(Drone drone)
        {
            //_player.BuildBudget.DecreaseBudget(drone.DroneConfigData.Cost);
            _player.BuildBudget.DecreaseBudget(drone.DecorableDrone.Cost);
            drone.transform.SetParent(transform);
            _droneOnBench = drone;
            OnDroneOnBenchChanged?.Invoke(drone);
        }

        private void RemoveFromBench(Drone drone)
        {
            //_player.BuildBudget.IncreaseBudget(drone.DroneConfigData.Cost);
            _player.BuildBudget.IncreaseBudget(drone.DecorableDrone.Cost);
            drone.transform.SetParent(null);
        }

        private void ResetCurrentDroneConfig()
        {
            if (_droneOnBench != null)
            {
                _droneOnBench.ResetConfiguration();
            }
        }
    
        private void DeleteCurrentDrone()
        {
            RemoveFromBench(_droneOnBench);
            Destroy(_droneOnBench.gameObject);
            OnDroneOnBenchChanged?.Invoke(null);
        }

        public void SpawnDronePrefab(GameObject prefab)
        {
            if (_droneOnBench != null)
            {
                DeleteCurrentDrone();
            }

            var droneGameObject = Instantiate(prefab);
            droneGameObject.transform.position = droneSpawnPosition.position;
            droneGameObject.layer = LayerMask.NameToLayer("Focus");
            SetChildLayersRecursively(droneGameObject.transform, "Focus");
            var drone = droneGameObject.GetComponent<Drone>();
            AddToBench(drone);
        }

        private void AddDroneToFleet() // Separate save method?
        {
            Debug.Log($"{_droneOnBench.DroneConfigData.droneName} <color=green>added to fleet</color>");
            SetChildLayersRecursively(_droneOnBench.transform, "Default");
            _player.DroneSwarm.AddToSwarm(_droneOnBench);
            
            // Save the customised drone as a prefab
            string droneName = "TestDrone";
            string path = $"Assets/Art/Prefabs/Drone/Class/Custom/{droneName}.prefab";
            _droneOnBench.transform.SetParent(null);
            PrefabUtility.SaveAsPrefabAsset(_droneOnBench.gameObject, path); // Overwrites if prefab already exists

            DeleteCurrentDrone();
            
            //SaveToFile();
            //DroneSaveSystem.SaveToFile(_droneOnBench.DecorableDrone);
        }

        private void SetChildLayersRecursively(Transform droneObject, string layer)
        {
            foreach (Transform child in droneObject)
            {
                child.gameObject.layer = LayerMask.NameToLayer(layer); 
                // recursively call SetLayersRecursively for each child
                SetChildLayersRecursively(child, layer);
            }
        }
    }
}
