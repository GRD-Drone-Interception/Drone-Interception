using System;
using System.Collections.Generic;
using DroneLoadout.DroneWorkbench;
using DroneLoadout.Scripts;
using SavingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopUi : MonoBehaviour // Workshop button manager?
{
    /*[SerializeField] private Button buyButton; 
    [SerializeField] private Button sellButton;
    [SerializeField] private Button exitEditButton;
    [SerializeField] private Button editDroneButton;
    [SerializeField] private Button resetDroneConfigButton;
    private readonly List<DroneModelSpawner> _droneModelSpawners = new();
    private Workbench _workbench;

    private void Awake()
    {
        _workbench = GetComponent<Workbench>();
        _droneModelSpawners.AddRange(FindObjectsOfType<DroneModelSpawner>());
    }

    private void OnEnable()
    {
        buyButton.onClick.AddListener(BuyDrone);
        sellButton.onClick.AddListener(SellDrone);
        resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
        _droneModelSpawners.ForEach(ctx => ctx.OnDroneModelSelected += BuildDrone);
        WorkshopModeController.OnModeChange -= OnWorkshopModeChanged;
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
                    _workbench.PurchaseDrone(droneData.droneCost);
                }
            }
        }
    }

    private void BuyDrone()
    {
        buyButton.GetComponentInChildren<TMP_Text>().text = "MODIFY";
        sellButton.gameObject.SetActive(true);
        exitEditButton.gameObject.SetActive(false);
        
        // TODO: Clean up
        foreach (var modelSpawner in _droneModelSpawners)
        {
            if (_workbench.DroneOnBench.DroneConfigData.DroneName == modelSpawner.GetDroneModelName())
            {
                if (!modelSpawner.IsPurchased())
                {
                    modelSpawner.SetPurchased(true);
                }
            }
        }
        
        _workbench.BuyDrone();
    }
    
    private void SellDrone()
    {
        buyButton.GetComponentInChildren<TMP_Text>().text = "BUY";
        buyButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(false);
        editDroneButton.gameObject.SetActive(true);
        exitEditButton.gameObject.SetActive(false);
        
        // TODO: Clean up
        foreach (var modelSpawner in _droneModelSpawners)
        {
            if (_workbench.DroneOnBench.DroneConfigData.DroneName == modelSpawner.GetDroneModelName())
            {
                if (modelSpawner.IsPurchased())
                {
                    modelSpawner.SetPurchased(false);
                }
            }
        }
        
        _workbench.SellDrone();
    }
    
    private void ResetCurrentDroneConfig()
    {
        _workbench.ResetCurrentDroneConfig();
    }
    
    private void BuildDrone(GameObject prefab)
    {
        if (DroneSaveSystem.CheckFileExists(_workbench.DroneOnBench.DroneConfigData.DroneName))
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "MODIFY";
            sellButton.gameObject.SetActive(true);
        }
        else
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "BUY";
            sellButton.gameObject.SetActive(false);
        }
        buyButton.gameObject.SetActive(true);
        editDroneButton.gameObject.SetActive(true);
        exitEditButton.gameObject.SetActive(false);
        
        _workbench.BuildDrone(prefab);
    }

    private void OnWorkshopModeChanged(WorkshopModeController.WorkshopMode mode)
    {
        if (mode == WorkshopModeController.WorkshopMode.Display)
        {
            buyButton.gameObject.SetActive(true);
            resetDroneConfigButton.gameObject.SetActive(false);
            exitEditButton.gameObject.SetActive(false);

            if (DroneSaveSystem.CheckFileExists(_workbench.DroneOnBench.DroneConfigData.DroneName))
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
    }*/
}
