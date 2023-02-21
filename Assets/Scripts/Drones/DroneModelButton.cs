using DroneWorkbench;
using UnityEngine;
using UnityEngine.UI;

namespace Drones
{
    public class DroneModelButton : MonoBehaviour
    {
        [SerializeField] private GameObject droneTypePrefab;
        [SerializeField] private DroneTypeButton droneTypeButton;
        [SerializeField] private DroneAttachmentSlot droneComponentSlot;
        private Button _modelButton;
        private Workbench _workbench;

        private void OnEnable() => _modelButton.onClick.AddListener(SpawnDroneModel);
        private void OnDisable() => _modelButton.onClick.RemoveListener(SpawnDroneModel);

        public void Awake()
        {
            _workbench = FindObjectOfType<Workbench>();
            _modelButton = GetComponent<Button>();
        }

        private void Start()
        {
            droneComponentSlot.gameObject.SetActive(false);
            droneComponentSlot.HideComponentSubMenu();
        }

        private void SpawnDroneModel()
        {
            droneTypeButton.HideModelSubMenu();
            _workbench.SpawnDronePrefab(droneTypePrefab);
            droneComponentSlot.gameObject.SetActive(true);
            droneComponentSlot.BindToDrone(_workbench.DroneBeingEdited);
            droneComponentSlot.BindToAttachmentPoint(_workbench.DroneBeingEdited.GetAttachmentPoints()[0]);
        }
    }
}