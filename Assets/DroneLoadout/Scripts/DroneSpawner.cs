using System;
using DroneLoadout.Decorators;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout.Scripts
{
    public class DroneSpawner : MonoBehaviour
    {
        public event Action<DroneSpawner> OnDroneSpawnerSelected;
        public DroneConfigData DroneConfigData => droneConfigData;

        [SerializeField] private DroneConfigData droneConfigData;
        private Button _spawnerButton;

        public void Awake() => _spawnerButton = GetComponent<Button>();
        private void OnEnable() => _spawnerButton.onClick.AddListener(SelectedDrone);
        private void OnDisable() => _spawnerButton.onClick.RemoveListener(SelectedDrone);

        private void SelectedDrone()
        {
            OnDroneSpawnerSelected?.Invoke(this);
        }
    }
}