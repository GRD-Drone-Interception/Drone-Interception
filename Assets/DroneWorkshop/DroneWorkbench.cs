using System;
using System.Collections.Generic;
using Core;
using DroneLoadout.Scripts;
using Testing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace DroneWorkshop
{
    /// <summary>
    /// Responsible for adding, removing, decorating, and resetting drone
    /// objects that have been added to the workbench. 
    /// </summary>
    public class DroneWorkbench : MonoBehaviour
    {
        public event Action<Drone> OnDroneChanged;
        public event Action<float> OnDronePurchased; 
        public event Action<float> OnDroneSold;
        public Drone DroneOnBench { get; private set; }
        
        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Player player;
        [SerializeField] private Button buyButton; // TODO: Abstract buttons into own class?
        [SerializeField] private Button sellButton;
        [SerializeField] private Button exitEditButton;
        [SerializeField] private Button editDroneButton;
        [SerializeField] private Button resetDroneConfigButton;
        private readonly List<DroneModelSpawner> _droneModelSpawners = new();

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
                if (JsonFileHandler.CheckFileExists(modelSpawner.GetDroneModelName()))
                {
                    if (!modelSpawner.IsPurchased())
                    {
                        modelSpawner.SetPurchased(true);
                    }
                }
            }
        }

        private void ResetCurrentDroneConfig()
        {
            if (DroneOnBench != null)
            {
                DroneOnBench.ResetConfiguration();
            }
        }

        private void BuildDrone(GameObject prefab)
        {
            if (DroneOnBench != null)
            {
                DroneOnBench.transform.SetParent(null);
                Destroy(DroneOnBench.gameObject);
            }
            var droneGameObject = Instantiate(prefab);
            var drone = droneGameObject.GetComponent<Drone>();
            drone.Rb.constraints = RigidbodyConstraints.FreezeAll;
            droneGameObject.transform.position = droneSpawnPosition.position;
            droneGameObject.transform.rotation = droneSpawnPosition.rotation;
            droneGameObject.layer = LayerMask.NameToLayer("Focus");
            TransformUtility.SetChildLayersIteratively(droneGameObject.transform, "Focus");
            drone.transform.SetParent(transform);
            DroneOnBench = drone;
            OnDroneChanged?.Invoke(drone);

            if (JsonFileHandler.CheckFileExists(DroneOnBench.DroneConfigData.DroneName))
            {
                buyButton.GetComponentInChildren<TMP_Text>().text = "MODIFY";
                sellButton.gameObject.SetActive(true);
                DroneLoader.Assemble(DroneOnBench);
            }
            else
            {
                buyButton.GetComponentInChildren<TMP_Text>().text = "BUY";
                sellButton.gameObject.SetActive(false);
            }
            buyButton.gameObject.SetActive(true);
            editDroneButton.gameObject.SetActive(true);
            exitEditButton.gameObject.SetActive(false);
        }

        private void BuyDrone()
        {
            if (!player.BuildBudget.CanAfford(DroneOnBench.DecorableDrone.Cost))
            {
                Debug.Log("You cannot afford this purchase!");
                return;
            }
            
            if (JsonFileHandler.CheckFileExists(DroneOnBench.DroneConfigData.DroneName))
            {
                // Sell existing drone
                DroneData savedData = JsonFileHandler.Load<DroneData>(DroneOnBench.DroneConfigData.DroneName);
                OnDroneSold?.Invoke(savedData.droneCost);
            }

            // TODO: Clean up
            foreach (var modelSpawner in _droneModelSpawners)
            {
                if (DroneOnBench.DroneConfigData.DroneName == modelSpawner.GetDroneModelName())
                {
                    if (!modelSpawner.IsPurchased())
                    {
                        modelSpawner.SetPurchased(true);
                    }
                }
            }
            SaveDroneData();
            OnDronePurchased?.Invoke(DroneOnBench.DecorableDrone.Cost);
        }

        private void SellDrone()
        {
            if (JsonFileHandler.CheckFileExists(DroneOnBench.DroneConfigData.DroneName)) 
            {
                OnDroneSold?.Invoke(DroneOnBench.DecorableDrone.Cost);
                DeleteDroneData();
                
                // TODO: Clean up
                foreach (var modelSpawner in _droneModelSpawners)
                {
                    if (DroneOnBench.DroneConfigData.DroneName == modelSpawner.GetDroneModelName())
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
            // Assemble the drone data
            DroneData droneData = new DroneData();
            droneData.droneCost = DroneOnBench.DecorableDrone.Cost;
            droneData.droneType = droneData.droneType;
            droneData.numAttachments = DroneOnBench.GetAttachmentPoints().Count;
            droneData.attachmentDictionaries = new List<AttachmentDictionary>();
            droneData.attachmentDataPaths = new List<string>();
            droneData.decalColour = DroneOnBench.GetPaintJob();
            
            int i = 0;
            foreach (var mountedAttachmentIndex in DroneOnBench.GetAttachmentPointTypeIndex().Keys)
            {
                AttachmentDictionary attachmentDictionary = new AttachmentDictionary();
                attachmentDictionary.attachmentPointIndex = mountedAttachmentIndex;
                attachmentDictionary.attachmentType = DroneOnBench.GetAttachmentPointTypeIndex()[mountedAttachmentIndex];
                droneData.attachmentDictionaries.Add(attachmentDictionary);
                droneData.attachmentDataPaths.Add(DroneOnBench.GetAttachmentPoints()[mountedAttachmentIndex].GetDroneAttachment().Data.PrefabDataPath);
                i++;
            }
            
            // Save and write the data to the chosen file location
            JsonFileHandler.Save(droneData, DroneOnBench.DroneConfigData.DroneName);

            buyButton.GetComponentInChildren<TMP_Text>().text = "MODIFY";
            sellButton.gameObject.SetActive(true);
            exitEditButton.gameObject.SetActive(false);
            Debug.Log("This drone's data has been saved to disk!");
        }

        private void DeleteDroneData()
        {
            if (!JsonFileHandler.CheckFileExists(DroneOnBench.DroneConfigData.DroneName))
            {
                Debug.Log("No saved data exists for this drone yet!");
                return;
            }
            JsonFileHandler.Delete(DroneOnBench.DroneConfigData.DroneName);
            buyButton.GetComponentInChildren<TMP_Text>().text = "BUY";
            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(false);
            editDroneButton.gameObject.SetActive(true);
            exitEditButton.gameObject.SetActive(false);
            ResetCurrentDroneConfig();
            Debug.Log("Saved data for this drone has been deleted!");
        }

        private void OnWorkshopModeChanged(WorkshopModeController.WorkshopMode mode)
        {
            if (mode == WorkshopModeController.WorkshopMode.Display)
            {
                buyButton.gameObject.SetActive(true);
                resetDroneConfigButton.gameObject.SetActive(false);
                exitEditButton.gameObject.SetActive(false);

                if (JsonFileHandler.CheckFileExists(DroneOnBench.DroneConfigData.DroneName))
                {
                    buyButton.GetComponentInChildren<TMP_Text>().text = "MODIFY";
                    sellButton.gameObject.SetActive(true);
                }
                else
                {
                    buyButton.GetComponentInChildren<TMP_Text>().text = "BUY";
                    sellButton.gameObject.SetActive(false);
                }
            }
            else if (mode == WorkshopModeController.WorkshopMode.Edit)
            {
                resetDroneConfigButton.gameObject.SetActive(true);
                exitEditButton.gameObject.SetActive(true);
                buyButton.gameObject.SetActive(false);
                sellButton.gameObject.SetActive(false);
            }
        }
    }
}
