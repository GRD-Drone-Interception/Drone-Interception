using System;
using Core;
using DroneLoadout.Scripts;
using Testing;
using UnityEngine;
using Utility;

namespace DroneWorkshop.Scripts
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

        private void OnEnable() => WorkshopModeController.OnModeChange += OnWorkshopModeChanged;
        private void OnDisable() => WorkshopModeController.OnModeChange -= OnWorkshopModeChanged;

        public void BuyDrone()
        {
            if (!player.BuildBudget.CanAfford(DroneOnBench.DecorableDrone.Cost))
            {
                Debug.Log("You cannot afford this purchase!");
                return;
            }
            
            if (JsonFileHandler.CheckFileExists(DroneOnBench.GetName()))
            {
                // Sell existing drone
                DroneData savedData = JsonFileHandler.Load<DroneData>(DroneOnBench.GetName());
                OnDroneSold?.Invoke(savedData.droneCost);
            }
            
            SaveDroneData();
            DroneOnBench.RemoveBlueprintShader();
            OnDronePurchased?.Invoke(DroneOnBench.DecorableDrone.Cost);
        }

        public void SellDrone()
        {
            if (JsonFileHandler.CheckFileExists(DroneOnBench.GetName())) 
            {
                OnDroneSold?.Invoke(DroneOnBench.DecorableDrone.Cost);
                DeleteDroneData();
                DroneOnBench.ApplyBlueprintShader();
                return;
            }
            Debug.Log("You haven't purchased this type of drone yet, so you can't sell it!");
        }

        public void ResetCurrentDrone()
        {
            if (DroneOnBench != null)
            {
                DroneOnBench.ResetConfiguration();
            }
        }

        public void BuildDrone(GameObject prefab)
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

            if (JsonFileHandler.CheckFileExists(DroneOnBench.GetName()))
            {
                DroneOnBench.RemoveBlueprintShader();
            }
            else
            {
                DroneOnBench.ApplyBlueprintShader();
            }
        }

        private void SaveDroneData() 
        {
            // Assemble the drone data
            DroneData droneData = new DroneData(DroneOnBench);

            // Save and write the data to the chosen file location
            JsonFileHandler.Save(droneData, droneData.droneName);
        }

        private void DeleteDroneData()
        {
            if (!JsonFileHandler.CheckFileExists(DroneOnBench.GetName()))
            {
                Debug.Log("No saved data exists for this drone yet!");
                return;
            }
            JsonFileHandler.Delete(DroneOnBench.GetName());
            ResetCurrentDrone();
            Debug.Log("Saved data for this drone has been deleted!");
        }

        private void OnWorkshopModeChanged(WorkshopModeController.WorkshopMode mode)
        {
            if (mode == WorkshopModeController.WorkshopMode.Display)
            {
                if (JsonFileHandler.CheckFileExists(DroneOnBench.GetName()))
                {
                    DroneOnBench.RemoveBlueprintShader();
                }
                else
                {
                    DroneOnBench.ApplyBlueprintShader();
                }
            }
            else if (mode == WorkshopModeController.WorkshopMode.Edit)
            {
                DroneOnBench.ApplyBlueprintShader();
            }
        }
    }
}
