using System;
using DroneSetup.DroneWorkbench;
using UnityEngine;
using UnityEngine.UI;

namespace DroneSetup
{
    /// <summary>
    /// Responsible for spawning a drone model onto the workbench.
    /// </summary>
    public class DroneModelButton : MonoBehaviour
    {
        public event Action<Drone> OnDroneModelSelected;
        
        [SerializeField] private GameObject droneTypePrefab;
        [SerializeField] private DroneTypeButton droneTypeButton;
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
            droneTypeButton.HideModelSubMenu();
            _workbench.SpawnDronePrefab(droneTypePrefab);
            OnDroneModelSelected?.Invoke(_workbench.DroneBeingEdited);
        }
    }
}