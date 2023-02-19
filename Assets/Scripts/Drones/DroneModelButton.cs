using DroneWorkbench;
using UnityEngine;
using UnityEngine.UI;

namespace Drones
{
    public class DroneModelButton : MonoBehaviour
    {
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
            droneTypeButton.HideSubMenu();
            _workbench.SpawnDronePrefab(droneTypePrefab);
        }
    }
}