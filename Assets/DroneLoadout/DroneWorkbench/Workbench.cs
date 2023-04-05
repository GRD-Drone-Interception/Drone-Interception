using System;
using System.Collections.Generic;
using DroneLoadout.Scripts;
using SavingSystem;
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
        public event Action<Drone> OnDroneAdded; 
        public event Action<Drone> OnDroneRemoved; 
        public event Action OnDroneOnBenchDestroyed; 
        public Drone DroneOnBench => _droneOnBench;

        [SerializeField] private GameObject whiteboard; // TODO: Move this 
        [SerializeField] private GameObject droneDataInfoBox; // TODO: Move this 
        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Button saveDeleteButton;
        [SerializeField] private Button editDroneButton;
        [SerializeField] private Button resetDroneConfigButton;
        private readonly List<DroneModelSpawner> _droneModelSpawners = new();
        private Drone _droneOnBench;

        private void Awake() => _droneModelSpawners.AddRange(FindObjectsOfType<DroneModelSpawner>());

        private void OnEnable()
        {
            saveDeleteButton.onClick.AddListener(AddDroneToFleet);
            resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
            _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected += BuildDrone);
        }

        private void OnDisable()
        {
            saveDeleteButton.onClick.RemoveListener(AddDroneToFleet);
            resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
            _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected -= BuildDrone);
        }

        private void Start() => resetDroneConfigButton.gameObject.SetActive(false);

        private void Update()
        {
            // TODO: Call this on an event
            if (WorkshopModeController.currentWorkshopMode == WorkshopModeController.WorkshopMode.Display)
            {
                saveDeleteButton.gameObject.SetActive(_droneOnBench != null);
                editDroneButton.gameObject.SetActive(_droneOnBench != null);
            }
            
            // Temporary
            if (DroneSetupMenu.State == DroneSetupMenuStates.Workshop)
            {
                whiteboard.SetActive(true);
            }
            else
            {
                whiteboard.SetActive(false);
            }
            
            // Temporary
            if (WorkshopModeController.currentWorkshopMode == WorkshopModeController.WorkshopMode.Edit)
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
            drone.transform.SetParent(transform);
            _droneOnBench = drone;
            OnDroneAdded?.Invoke(drone);
        }

        private void RemoveFromBench(Drone drone)
        {
            drone.transform.SetParent(null);
            OnDroneRemoved?.Invoke(drone);
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
            var drone = droneGameObject.GetComponent<Drone>();
            drone.Rb.constraints = RigidbodyConstraints.FreezeAll;
            droneGameObject.transform.position = droneSpawnPosition.position;
            droneGameObject.transform.rotation = droneSpawnPosition.rotation;
            droneGameObject.layer = LayerMask.NameToLayer("Focus");
            SetChildLayersIteratively(droneGameObject.transform, "Focus");
            AddToBench(drone);
        }

        private void AddDroneToFleet() 
        {
            _droneOnBench.Rb.constraints = RigidbodyConstraints.None;
            SetChildLayersIteratively(_droneOnBench.transform, "Default");
            DroneSaveSystem.Save(_droneOnBench);
            DeleteCurrentDrone();
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
