using System;
using DroneLoadout.Decorators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout.Scripts
{
    /// <summary>
    /// Responsible for spawning a drone model onto the workbench.
    /// </summary>
    public class DroneModelSpawner : MonoBehaviour
    {
        public event Action<GameObject> OnDroneModelSelected;
        
        [SerializeField] private DroneConfigData droneConfigData;
        [SerializeField] private DroneTypeSelector droneTypeSelector;
        private Button _modelButton;
        private TMP_Text _text;
        
        public void Awake()
        {
            _modelButton = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnValidate()
        {
            if (droneConfigData == null) { return;}
            _text = GetComponentInChildren<TMP_Text>();
            _text.text = droneConfigData.DroneName;
        }

        private void OnEnable() => _modelButton.onClick.AddListener(SpawnDroneModel);
        private void OnDisable() => _modelButton.onClick.RemoveListener(SpawnDroneModel);

        private void SpawnDroneModel()
        {
            droneTypeSelector.HideModelSubMenu();
            OnDroneModelSelected?.Invoke(droneConfigData.DronePrefab);
        }
    }
}