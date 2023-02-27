using System;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for spawning a drone model onto the workbench.
    /// </summary>
    public class DroneModelSpawner : MonoBehaviour
    {
        public event Action<GameObject> OnDroneModelSelected;
        
        [SerializeField] private GameObject droneModelPrefab;
        [SerializeField] private DroneTypeSelector droneTypeSelector;
        private Button _modelButton;

        private void OnEnable() => _modelButton.onClick.AddListener(SpawnDroneModel);
        private void OnDisable() => _modelButton.onClick.RemoveListener(SpawnDroneModel);

        public void Awake()
        {
            _modelButton = GetComponent<Button>();
        }

        private void SpawnDroneModel()
        {
            droneTypeSelector.HideModelSubMenu();
            OnDroneModelSelected?.Invoke(droneModelPrefab);
        }
    }
}