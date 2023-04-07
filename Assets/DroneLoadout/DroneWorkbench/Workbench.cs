using System;
using System.Collections.Generic;
using System.Linq;
using DroneLoadout.Scripts;
using SavingSystem;
using UnityEngine;
using UnityEngine.Serialization;
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
        public event Action<float> OnDronePurchased; 
        public event Action<float> OnDroneSold; 
        public event Action OnDroneOnBenchDestroyed; 
        public Drone DroneOnBench => _droneOnBench;

        [SerializeField] private GameObject whiteboard; // TODO: Move this 
        [SerializeField] private GameObject droneDataInfoBox; // TODO: Move this 
        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Button buyButton; // TODO: Abstract buttons into own class?
        [SerializeField] private Button sellButton;
        [SerializeField] private Button exitEditButton;
        [SerializeField] private Button editDroneButton;
        [SerializeField] private Button resetDroneConfigButton;
        private readonly List<DroneModelSpawner> _droneModelSpawners = new();
        private Drone _droneOnBench;

        private void Awake() => _droneModelSpawners.AddRange(FindObjectsOfType<DroneModelSpawner>());

        private void OnEnable()
        {
            buyButton.onClick.AddListener(BuyDrone); 
            sellButton.onClick.AddListener(SellDrone);
            resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
            _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected += BuildDrone);
            WorkshopModeController.OnModeChange += OnWorkshopModeChanged;
        }

        private void OnDisable()
        {
            buyButton.onClick.RemoveListener(BuyDrone);
            sellButton.onClick.RemoveListener(SellDrone);
            resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
            _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected -= BuildDrone);
            WorkshopModeController.OnModeChange -= OnWorkshopModeChanged;
        }

        private void Start()
        {
            editDroneButton.gameObject.SetActive(false);
            exitEditButton.gameObject.SetActive(false);
            resetDroneConfigButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);

            foreach (var modelSpawner in _droneModelSpawners)
            {
                if (DroneSaveSystem.CheckFileExists(modelSpawner.GetDroneModelName()))
                {
                    if (!modelSpawner.IsPurchased())
                    {
                        modelSpawner.SetPurchased(true);
                        
                        DroneData droneData = DroneSaveSystem.Load(modelSpawner.GetDroneModelName());
                        OnDronePurchased?.Invoke(droneData.droneCost);
                    }
                }
            }
        }

        private void Update()
        {
            // Temporary
            if (DroneSetupMenu.State == DroneSetupMenuStates.Workshop)
            {
                whiteboard.SetActive(true);
            }
            else
            {
                whiteboard.SetActive(false);
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

            if (DroneSaveSystem.CheckFileExists(_droneOnBench.DroneConfigData.DroneName))
            {
                buyButton.gameObject.SetActive(false);
                sellButton.gameObject.SetActive(true);
                DroneSavedAttachmentsAssembler.BuildDrone(_droneOnBench);
            }
            else
            {
                buyButton.gameObject.SetActive(true);
                sellButton.gameObject.SetActive(false);
            }
            editDroneButton.gameObject.SetActive(true);
            exitEditButton.gameObject.SetActive(false);
        }

        private void BuyDrone()
        {
            SaveDroneData();
            OnDronePurchased?.Invoke(_droneOnBench.DecorableDrone.Cost);

            // TODO: Clean up
            foreach (var modelSpawner in _droneModelSpawners)
            {
                if (_droneOnBench.DroneConfigData.DroneName == modelSpawner.GetDroneModelName())
                {
                    if (!modelSpawner.IsPurchased())
                    {
                        modelSpawner.SetPurchased(true);
                    }
                }
            }
        }

        private void SellDrone()
        {
            if (DroneSaveSystem.CheckFileExists(_droneOnBench.DroneConfigData.DroneName)) 
            {
                OnDroneSold?.Invoke(_droneOnBench.DecorableDrone.Cost);
                DeleteDroneData();
                
                // TODO: Clean up
                foreach (var modelSpawner in _droneModelSpawners)
                {
                    if (_droneOnBench.DroneConfigData.DroneName == modelSpawner.GetDroneModelName())
                    {
                        if (modelSpawner.IsPurchased())
                        {
                            modelSpawner.SetPurchased(false);
                        }
                    }
                }
                
                return;
            }
            exitEditButton.gameObject.SetActive(false);
            Debug.Log("You haven't purchased this type of drone yet, so you can't sell it!");
        }

        private void SaveDroneData() 
        {
            DroneSaveSystem.Save(_droneOnBench);
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(true);
            exitEditButton.gameObject.SetActive(true);
            Debug.Log("This drone's data has been saved to disk!");
        }

        private void DeleteDroneData()
        {
            if (!DroneSaveSystem.CheckFileExists(_droneOnBench.DroneConfigData.DroneName))
            {
                Debug.Log("No saved data exists for this drone yet!");
                return;
            }
            DroneSaveSystem.Delete(_droneOnBench);
            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(false);
            exitEditButton.gameObject.SetActive(false);
            ResetCurrentDroneConfig();
            Debug.Log("Saved data for this drone has been deleted!");
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
        
        private void OnWorkshopModeChanged(WorkshopModeController.WorkshopMode mode)
        {
            if (mode == WorkshopModeController.WorkshopMode.Display)
            {
                resetDroneConfigButton.gameObject.SetActive(false);
                editDroneButton.gameObject.SetActive(true);
                exitEditButton.gameObject.SetActive(false);
                droneDataInfoBox.SetActive(false);

                if (DroneSaveSystem.CheckFileExists(_droneOnBench.DroneConfigData.DroneName))
                {
                    buyButton.gameObject.SetActive(false);
                    sellButton.gameObject.SetActive(true);
                }
                else
                {
                    buyButton.gameObject.SetActive(true);
                    sellButton.gameObject.SetActive(false);
                }
            }
            else if (mode == WorkshopModeController.WorkshopMode.Edit)
            {
                resetDroneConfigButton.gameObject.SetActive(true);
                editDroneButton.gameObject.SetActive(false);
                exitEditButton.gameObject.SetActive(true);
                droneDataInfoBox.SetActive(true);
                buyButton.gameObject.SetActive(false);
                sellButton.gameObject.SetActive(false);
            }
        }
    }
}
