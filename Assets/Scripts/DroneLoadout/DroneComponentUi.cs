using System.Collections.Generic;
using DroneLoadout.Decorators;
using UnityEngine;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for hiding and displaying attachment slots and drone type
    /// buttons based on the drone loadout camera mode.
    /// </summary>
    public class DroneComponentUi : MonoBehaviour
    {
        [SerializeField] private Transform workshopCanvas;
        [SerializeField] private List<GameObject> droneAttachmentSlotPrefab;
        //private readonly List<DroneAttachmentSlot> _attachmentSlots = new();
        private readonly List<DroneTypeSelector> _droneTypeButtons = new();
        private readonly List<DroneModelSpawner> _droneModelButtons = new();
        private Dictionary<DroneAttachmentType, GameObject> _droneComponentTypePrefabDict;

        private void Awake()
        {
            _droneTypeButtons.AddRange(FindObjectsOfType<DroneTypeSelector>());
            _droneModelButtons.AddRange(FindObjectsOfType<DroneModelSpawner>());
        }

        private void Start()
        {
            /*_droneComponentTypePrefabDict = new Dictionary<DroneAttachmentType, GameObject>
            {
                {DroneAttachmentType.Camera, droneAttachmentSlotPrefab[0]},
                {DroneAttachmentType.Battery, droneAttachmentSlotPrefab[1]},
                {DroneAttachmentType.Motor, droneAttachmentSlotPrefab[2]},
                {DroneAttachmentType.Propeller, droneAttachmentSlotPrefab[3]},
                {DroneAttachmentType.FlightController, droneAttachmentSlotPrefab[4]},
                {DroneAttachmentType.Radar, droneAttachmentSlotPrefab[5]},
                {DroneAttachmentType.Sensor, droneAttachmentSlotPrefab[6]},
                {DroneAttachmentType.Payload, droneAttachmentSlotPrefab[7]},
                {DroneAttachmentType.Weapon, droneAttachmentSlotPrefab[8]}
            };*/
            _droneComponentTypePrefabDict = new Dictionary<DroneAttachmentType, GameObject>();
            for (var i = 0; i < droneAttachmentSlotPrefab.Count; i++)
            {
                _droneComponentTypePrefabDict.Add((DroneAttachmentType)i, droneAttachmentSlotPrefab[i]);
            }
        }

        private void OnEnable()
        {
            DroneLoadoutCameraMode.OnModeChange += OnDroneLoadoutCameraModeChange;
            _droneModelButtons.ForEach(button => button.OnDroneModelSpawned += OnDroneModelSelected);
        }

        private void OnDisable()
        {
            DroneLoadoutCameraMode.OnModeChange -= OnDroneLoadoutCameraModeChange;
            _droneModelButtons.ForEach(button => button.OnDroneModelSpawned -= OnDroneModelSelected);
        }

        private void OnDroneLoadoutCameraModeChange(DroneLoadoutCameraMode.CameraMode mode)
        {
            switch (mode)
            {
                case DroneLoadoutCameraMode.CameraMode.Edit:
                   //_attachmentSlots.ForEach(slot => slot.gameObject.SetActive(true));
                    _droneTypeButtons.ForEach(slot => slot.gameObject.SetActive(false));
                    break;
                case DroneLoadoutCameraMode.CameraMode.Display:
                    //_attachmentSlots.ForEach(slot => slot.gameObject.SetActive(false));
                    //_attachmentSlots.ForEach(slot => slot.HideComponentSubMenu());
                    _droneTypeButtons.ForEach(slot => slot.gameObject.SetActive(true));
                    break;
            }
        }
        
        private void OnDroneModelSelected(Drone drone)
        {
            // Destroy existing attachment slot ui buttons
            foreach (var droneAttachmentSlot in FindObjectsOfType<DroneAttachmentSlot>())
            {
                Destroy(droneAttachmentSlot.gameObject);
            }

            for (var i = 0; i < drone.GetAttachmentPoints().Count; i++)
            {
                var droneAttachmentSlotUiButton = Instantiate(_droneComponentTypePrefabDict[drone.GetAttachmentPoints()[i].GetAttachmentType()]); 
                var uiButton = droneAttachmentSlotUiButton.transform;
                uiButton.position = new Vector3(uiButton.position.x, uiButton.position.y - (i * 150), uiButton.position.z);
                droneAttachmentSlotUiButton.transform.SetParent(workshopCanvas);

                var droneAttachmentSlot = droneAttachmentSlotUiButton.GetComponent<DroneAttachmentSlot>();
                droneAttachmentSlot.BindToDrone(drone);
                droneAttachmentSlot.BindToAttachmentPoint(drone.GetAttachmentPoints()[i]);
            }
        }
    }
}
