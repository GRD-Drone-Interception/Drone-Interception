using System.Collections.Generic;
using System.Linq;
using DroneLoadout.Decorators;
using DroneLoadout.DroneWorkbench;
using UnityEngine;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for spawning and setting the visibility of drone attachment slots
    /// </summary>
    public class DroneAttachmentSlotSpawner : MonoBehaviour
    {
        [SerializeField] private Transform workshopCanvas;
        [SerializeField] private List<GameObject> droneAttachmentSlotTypePrefabs;
        private Dictionary<DroneAttachmentType, GameObject> _droneAttachmentTypePrefabDict;
        private readonly List<DroneAttachmentSlot> _droneAttachmentSlots = new();
        private Workbench _workbench;

        private void Awake() => _workbench = FindObjectOfType<Workbench>();

        private void OnEnable()
        {
            WorkshopModeController.OnModeChange += SetVisibilityOfAttachmentSlots;
            _workbench.OnDroneAdded += SpawnAttachmentSlots;
        }

        private void OnDisable()
        {
            WorkshopModeController.OnModeChange -= SetVisibilityOfAttachmentSlots;
            _workbench.OnDroneAdded -= SpawnAttachmentSlots;
        }

        private void Start()
        {
            // Checks attachment type of each attachment slot prefab and assigns it the corresponding key in the dictionary 
            _droneAttachmentTypePrefabDict = new Dictionary<DroneAttachmentType, GameObject>();
            foreach (var prefab in droneAttachmentSlotTypePrefabs)
            {
                switch (prefab.GetComponent<DroneAttachmentSlot>().AttachmentType)
                {
                    case DroneAttachmentType.Camera:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.Camera, prefab);
                        break;
                    case DroneAttachmentType.Battery:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.Battery, prefab);
                        break;
                    case DroneAttachmentType.Motor:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.Motor, prefab);
                        break;
                    case DroneAttachmentType.Propeller:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.Propeller, prefab);
                        break;
                    case DroneAttachmentType.FlightController:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.FlightController, prefab);
                        break;
                    case DroneAttachmentType.Radar:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.Radar, prefab);
                        break;
                    case DroneAttachmentType.Sensor:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.Sensor, prefab);
                        break;
                    case DroneAttachmentType.Payload:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.Payload, prefab);
                        break;
                    case DroneAttachmentType.Weapon:
                        _droneAttachmentTypePrefabDict.Add(DroneAttachmentType.Weapon, prefab);
                        break;
                }
            }
        }

        private void SetVisibilityOfAttachmentSlots(WorkshopModeController.WorkshopMode mode)
        {
            switch (mode)
            {
                case WorkshopModeController.WorkshopMode.Edit:
                    _droneAttachmentSlots.ForEach(slot => slot.gameObject.SetActive(true));
                    break;
                case WorkshopModeController.WorkshopMode.Display:
                    _droneAttachmentSlots.ForEach(slot => slot.gameObject.SetActive(false));
                    _droneAttachmentSlots.ForEach(slot => slot.HideComponentSubMenu());
                    break;
            }
        }
        
        private void SpawnAttachmentSlots(Drone drone)
        {
            // Destroy existing attachment slot ui buttons
            foreach (var slot in _droneAttachmentSlots)
            {
                slot.OnAttachmentSlotSelected -= SetVisibilityOfUnselectedDroneAttachmentSlotSubMenus;
                Destroy(slot.gameObject);
            }
            _droneAttachmentSlots.Clear();

            for (var i = 0; i < drone.GetAttachmentPoints().Count; i++)
            {
                var droneAttachmentSlotUiButton = Instantiate(_droneAttachmentTypePrefabDict[drone.GetAttachmentPoints()[i].GetAttachmentType()]); 
                var uiButton = droneAttachmentSlotUiButton.transform;
                uiButton.position = new Vector3(uiButton.position.x, uiButton.position.y - (i * 100), uiButton.position.z);
                droneAttachmentSlotUiButton.transform.SetParent(workshopCanvas);

                var droneAttachmentSlot = droneAttachmentSlotUiButton.GetComponent<DroneAttachmentSlot>();
                droneAttachmentSlot.BindToDrone(drone);
                droneAttachmentSlot.BindToAttachmentPoint(drone.GetAttachmentPoints()[i]);
                _droneAttachmentSlots.Add(droneAttachmentSlot);
                droneAttachmentSlot.gameObject.SetActive(false);
                droneAttachmentSlot.HideComponentSubMenu();
                droneAttachmentSlot.OnAttachmentSlotSelected += SetVisibilityOfUnselectedDroneAttachmentSlotSubMenus;
            }
        }

        private void SetVisibilityOfUnselectedDroneAttachmentSlotSubMenus(DroneAttachmentSlot slot)
        {
            // Hide attachment slot sub-menus besides the one currently selected
            foreach (var button in _droneAttachmentSlots.Where(button => button != slot))
            {
                button.HideComponentSubMenu();
            }
        }

        private void OnDestroy() => _droneAttachmentSlots.ForEach(slot => slot.OnAttachmentSlotSelected -= SetVisibilityOfUnselectedDroneAttachmentSlotSubMenus);
    }
}
