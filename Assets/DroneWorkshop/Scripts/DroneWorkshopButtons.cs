using System.Collections.Generic;
using DroneLoadout.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace DroneWorkshop.Scripts
{
    /// <summary>
    /// Responsible for displaying the user interface for the drone workbench.
    /// </summary>
    public class DroneWorkshopButtons : MonoBehaviour
    {
        [SerializeField] private DroneWorkbench workbench;
        [SerializeField] private Button buyButton; 
        [SerializeField] private Button sellButton;
        [SerializeField] private Button editDroneButton;
        [SerializeField] private Button exitEditButton;
        [SerializeField] private Button resetDroneConfigButton;
        private readonly List<DroneModelSpawner> _droneModelSpawners = new();
        private TMP_Text _buyButtonText;

        private void Awake()
        {
            _droneModelSpawners.AddRange(FindObjectsOfType<DroneModelSpawner>());
            _buyButtonText = buyButton.GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            buyButton.onClick.AddListener(OnBuyButtonPressed); 
            sellButton.onClick.AddListener(OnSellButtonPressed);
            editDroneButton.onClick.AddListener(UpdateVisibilityOfEditButtons);
            exitEditButton.onClick.AddListener(UpdateVisibilityOfEditButtons);
            resetDroneConfigButton.onClick.AddListener(workbench.ResetCurrentDrone);
            _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected += OnDroneModelButtonsPressed);
            WorkshopModeController.OnModeChange += OnWorkshopModeChanged;
        }

        private void OnDisable()
        {
            buyButton.onClick.RemoveListener(OnBuyButtonPressed);
            sellButton.onClick.RemoveListener(OnSellButtonPressed);
            editDroneButton.onClick.RemoveListener(UpdateVisibilityOfEditButtons);
            exitEditButton.onClick.RemoveListener(UpdateVisibilityOfEditButtons);
            resetDroneConfigButton.onClick.RemoveListener(workbench.ResetCurrentDrone);
            _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected -= OnDroneModelButtonsPressed);
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

        private void OnBuyButtonPressed()
        {
            workbench.BuyDrone();
            _buyButtonText.text = "MODIFY";
            sellButton.gameObject.SetActive(true);
            exitEditButton.gameObject.SetActive(false);
            
            foreach (var modelSpawner in _droneModelSpawners)
            {
                if (workbench.DroneOnBench.GetName() == modelSpawner.GetDroneModelName())
                {
                    if (!modelSpawner.IsPurchased())
                    {
                        modelSpawner.SetPurchased(true);
                    }
                }
            }
        }

        private void OnSellButtonPressed()
        {
            workbench.SellDrone();
            sellButton.gameObject.SetActive(false);
            _buyButtonText.text = "BUY";

            if (!JsonFileHandler.CheckFileExists(workbench.DroneOnBench.GetName()))
            {
                exitEditButton.gameObject.SetActive(false);
            }
            
            foreach (var modelSpawner in _droneModelSpawners)
            {
                if (workbench.DroneOnBench.GetName() == modelSpawner.GetDroneModelName())
                {
                    if (modelSpawner.IsPurchased())
                    {
                        modelSpawner.SetPurchased(false);
                    }
                }
            }
        }
        
        private void OnDroneModelButtonsPressed(GameObject prefab)
        {
            workbench.BuildDrone(prefab);
            
            if (JsonFileHandler.CheckFileExists(workbench.DroneOnBench.GetName()))
            {
                _buyButtonText.text = "MODIFY";
                sellButton.gameObject.SetActive(true);
            }
            else
            {
                _buyButtonText.text = "BUY";
                sellButton.gameObject.SetActive(false);
            }
            buyButton.gameObject.SetActive(true);
            editDroneButton.gameObject.SetActive(true);
            exitEditButton.gameObject.SetActive(false);
        }
        
        private void UpdateVisibilityOfEditButtons()
        {
            editDroneButton.gameObject.SetActive(WorkshopModeController.currentWorkshopMode == WorkshopModeController.WorkshopMode.Display);
            exitEditButton.gameObject.SetActive(WorkshopModeController.currentWorkshopMode == WorkshopModeController.WorkshopMode.Edit);
        }
        
        private void OnWorkshopModeChanged(WorkshopModeController.WorkshopMode mode)
        {
            if (mode == WorkshopModeController.WorkshopMode.Display)
            {
                buyButton.gameObject.SetActive(true);
                resetDroneConfigButton.gameObject.SetActive(false);
                exitEditButton.gameObject.SetActive(false);

                if (JsonFileHandler.CheckFileExists(workbench.DroneOnBench.GetName()))
                {
                    _buyButtonText.text = "MODIFY";
                    sellButton.gameObject.SetActive(true);
                }
                else
                {
                    _buyButtonText.text = "BUY";
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
