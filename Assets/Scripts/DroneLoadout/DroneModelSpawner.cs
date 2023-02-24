using System;
using DroneLoadout.DroneWorkbench;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for spawning a drone model onto the workbench.
    /// </summary>
    public class DroneModelSpawner : MonoBehaviour
    {
        public event Action<Drone> OnDroneModelSpawned;
        
        [SerializeField] private GameObject droneTypePrefab;
        [SerializeField] private DroneTypeSelector droneTypeSelector;
        private Button _modelButton;
        private Workbench _workbench;

        private void OnEnable() => _modelButton.onClick.AddListener(SpawnDroneModel);
        private void OnDisable() => _modelButton.onClick.RemoveListener(SpawnDroneModel);

        public void Awake()
        {
            _workbench = FindObjectOfType<Workbench>();
            _modelButton = GetComponent<Button>();
        }

        private void SpawnDroneModel()
        {
            droneTypeSelector.HideModelSubMenu();
            _workbench.SpawnDronePrefab(droneTypePrefab);
            OnDroneModelSpawned?.Invoke(_workbench.DroneBeingEdited);
        }
    }
}