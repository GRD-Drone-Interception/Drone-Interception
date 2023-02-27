using System;
using System.Collections.Generic;
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
        public event Action OnDroneOnBenchDestroyed; 
        public Drone DroneOnBench => _droneOnBench;

        [SerializeField] private GameObject backboard; // TODO: Move this 
        [SerializeField] private GameObject droneDataInfoBox; // TODO: Move this 
        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Button addToFleetButton;
        [SerializeField] private Button editDroneButton;
        [SerializeField] private Button resetDroneConfigButton;
        private readonly List<DroneModelSpawner> _droneModelSpawners = new();
        private Drone _droneOnBench;
        private Player _player;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _droneModelSpawners.AddRange(FindObjectsOfType<DroneModelSpawner>());
        }

        private void OnEnable()
        {
            addToFleetButton.onClick.AddListener(AddDroneToFleet);
            resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
            _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected += BuildDrone);
        }

        private void OnDisable()
        {
            addToFleetButton.onClick.RemoveListener(AddDroneToFleet);
            resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
            _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected -= BuildDrone);
        }

        private void Start() => resetDroneConfigButton.gameObject.SetActive(false);

        private void Update()
        {
            // TODO: Call this on an event
            if (WorkshopModeController.currentWorkshopMode == WorkshopMode.Display)
            {
                addToFleetButton.gameObject.SetActive(_droneOnBench != null);
                editDroneButton.gameObject.SetActive(_droneOnBench != null);
            }
            
            // Temporary
            if (DroneSetupMenu.State == DroneSetupMenuStates.Workshop)
            {
                backboard.SetActive(true);
            }
            else
            {
                backboard.SetActive(false);
            }
            
            // Temporary
            if (WorkshopModeController.currentWorkshopMode == WorkshopMode.Edit)
            {
                droneDataInfoBox.SetActive(true);
            }
            else
            {
                droneDataInfoBox.SetActive(false);
            }
        }

        private void AddToBench(Drone drone)
        {
            _player.BuildBudget.Spend(drone.DecorableDrone.Cost);
            drone.transform.SetParent(transform);
            _droneOnBench = drone;
            OnDroneOnBenchChanged?.Invoke(drone);
        }

        private void RemoveFromBench(Drone drone)
        {
            _player.BuildBudget.Sell(drone.DecorableDrone.Cost);
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
            OnDroneOnBenchDestroyed?.Invoke();
        }

        private void BuildDrone(GameObject prefab)
        {
            if (_droneOnBench != null)
            {
                DeleteCurrentDrone();
            }

            var droneGameObject = Instantiate(prefab);
            droneGameObject.transform.position = droneSpawnPosition.position;
            droneGameObject.layer = LayerMask.NameToLayer("Focus");
            SetChildLayersIteratively(droneGameObject.transform, "Focus");
            var drone = droneGameObject.GetComponent<Drone>();
            AddToBench(drone);
        }

        private void AddDroneToFleet() // Separate save method?
        {
            Debug.Log($"{_droneOnBench.DroneConfigData.droneName} <color=green>added to fleet</color>");
            SetChildLayersIteratively(_droneOnBench.transform, "Default");
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

        /// <summary>
        /// Performs an iterative depth-first search traversal on a drone object hierarchy
        /// in order to assign each of it's children with a given layer
        /// </summary>
        /// <param name="droneObject"></param>
        /// <param name="layer"></param>
        private void SetChildLayersIteratively(Transform droneObject, string layer)
        {
            var stack = new Stack<Transform>();
            stack.Push(droneObject);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                current.gameObject.layer = LayerMask.NameToLayer(layer);
                for (int i = 0; i < current.childCount; i++)
                {
                    stack.Push(current.GetChild(i));
                }
            }
        }
    }
}
