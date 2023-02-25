using System.Collections.Generic;
using DroneLoadout.Decorators;
using UnityEngine;
using UnityEngine.Serialization;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for hiding and displaying attachment slots and drone type
    /// buttons based on the drone loadout camera mode.
    /// </summary>
    public class DroneComponentUi : MonoBehaviour
    {
        [SerializeField] private Transform workshopCanvas;
        [SerializeField] private List<GameObject> droneAttachmentSlotTypePrefabs;
        private Dictionary<DroneAttachmentType, GameObject> _droneAttachmentTypePrefabDict;
        private readonly List<DroneTypeSelector> _droneTypeButtons = new();
        private readonly List<DroneModelSpawner> _droneModelButtons = new();
        private readonly List<DroneAttachmentSlot> _droneAttachmentSlots = new();

        private void Awake()
        {
            _droneTypeButtons.AddRange(FindObjectsOfType<DroneTypeSelector>());
            _droneModelButtons.AddRange(FindObjectsOfType<DroneModelSpawner>());
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

        private void OnEnable()
        {
            DroneLoadoutCameraMode.OnModeChange += SetVisibilityOfDroneTypeButtons;
            _droneModelButtons.ForEach(button => button.OnDroneModelSpawned += SpawnAttachmentSlots);
        }

        private void OnDisable()
        {
            DroneLoadoutCameraMode.OnModeChange -= SetVisibilityOfDroneTypeButtons;
            _droneModelButtons.ForEach(button => button.OnDroneModelSpawned -= SpawnAttachmentSlots);
        }

        private void SetVisibilityOfDroneTypeButtons(DroneLoadoutCameraMode.CameraMode mode)
        {
            switch (mode)
            {
                case DroneLoadoutCameraMode.CameraMode.Edit:
                    _droneAttachmentSlots.ForEach(slot => slot.gameObject.SetActive(true));
                    _droneTypeButtons.ForEach(slot => slot.gameObject.SetActive(false));
                    break;
                case DroneLoadoutCameraMode.CameraMode.Display:
                    _droneAttachmentSlots.ForEach(slot => slot.gameObject.SetActive(false));
                    _droneAttachmentSlots.ForEach(slot => slot.HideComponentSubMenu());
                    _droneTypeButtons.ForEach(slot => slot.gameObject.SetActive(true));
                    break;
            }
        }
        
        private void SpawnAttachmentSlots(Drone drone)
        {
            // Destroy existing attachment slot ui buttons
            foreach (var slot in _droneAttachmentSlots)
            {
                Destroy(slot.gameObject);
            }
            _droneAttachmentSlots.Clear();

            for (var i = 0; i < drone.GetAttachmentPoints().Count; i++)
            {
                var droneAttachmentSlotUiButton = Instantiate(_droneAttachmentTypePrefabDict[drone.GetAttachmentPoints()[i].GetAttachmentType()]); 
                var uiButton = droneAttachmentSlotUiButton.transform;
                uiButton.position = new Vector3(uiButton.position.x, uiButton.position.y - (i * 150), uiButton.position.z);
                droneAttachmentSlotUiButton.transform.SetParent(workshopCanvas);

                var droneAttachmentSlot = droneAttachmentSlotUiButton.GetComponent<DroneAttachmentSlot>();
                droneAttachmentSlot.BindToDrone(drone);
                droneAttachmentSlot.BindToAttachmentPoint(drone.GetAttachmentPoints()[i]);
                _droneAttachmentSlots.Add(droneAttachmentSlot);
                droneAttachmentSlot.gameObject.SetActive(false);
                droneAttachmentSlot.HideComponentSubMenu();
            }
        }
    }
}
