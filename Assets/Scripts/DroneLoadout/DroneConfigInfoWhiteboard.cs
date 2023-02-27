using DroneLoadout.DroneWorkbench;
using TMPro;
using UnityEngine;

namespace DroneLoadout
{
    public class DroneConfigInfoWhiteboard : MonoBehaviour
    {
        [SerializeField] private TMP_Text droneTypeText;
        [SerializeField] private TMP_Text droneModelText;
        [SerializeField] private TMP_Text droneModsText;
        [SerializeField] private TMP_Text droneCostText;
        [SerializeField] private TMP_Text droneRangeText;
        [SerializeField] private TMP_Text droneSpeedText;
        [SerializeField] private TMP_Text droneAccelerationText;
        [SerializeField] private TMP_Text droneWeightText;
        private Workbench _workbench;

        private void Awake() => _workbench = FindObjectOfType<Workbench>();

        private void OnEnable()
        {
            _workbench.OnDroneOnBenchChanged += SubscribeToNewDronesDecoratedEvents;
            _workbench.OnDroneOnBenchDestroyed += ClearDroneInfoFromUI;
        }

        private void OnDisable()
        {
            _workbench.OnDroneOnBenchChanged -= SubscribeToNewDronesDecoratedEvents;
            _workbench.OnDroneOnBenchDestroyed -= ClearDroneInfoFromUI;
        }
        
        private void Start() => ClearDroneInfoFromUI();

        private void SubscribeToNewDronesDecoratedEvents(Drone drone)
        {
            UpdateDroneInfoUi(drone, null);
            _workbench.DroneOnBench.OnDroneDecorationAdded += UpdateDroneInfoUi;
            _workbench.DroneOnBench.OnDroneDecorationRemoved += UpdateDroneInfoUi;
        }

        private void UpdateDroneInfoUi(Drone drone, DroneAttachment droneAttachment)
        {
            droneTypeText.text = $"DRONE TYPE: {drone.DroneConfigData.droneType}";
            droneModelText.text = $"DRONE MODEL: {drone.DroneConfigData.droneName}";
            droneModsText.text = $"MODS: {drone.NumOfMountedAttachments}";
            droneCostText.text = $"COST: {drone.DecorableDrone.Cost:C0}";
            droneRangeText.text = $"RANGE: {drone.DecorableDrone.Range}km";
            droneSpeedText.text = $"SPEED: {drone.DecorableDrone.Speed}mph";
            droneAccelerationText.text = $"ACCELERATION: {drone.DecorableDrone.Acceleration}km/h in ?s";
            droneWeightText.text = $"WEIGHT: {drone.DecorableDrone.Weight}kg";
        }

        private void ClearDroneInfoFromUI()
        {
            droneTypeText.text = "DRONE TYPE: NONE";
            droneModelText.text = "DRONE MODEL: NONE";
            droneModsText.text = "MODS: 0";
            droneCostText.text = "COST: £0";
            droneRangeText.text = "RANGE: 0km";
            droneSpeedText.text = "SPEED: 0mph";
            droneAccelerationText.text = "ACCELERATION: 0km/h in ?s";
            droneWeightText.text = "WEIGHT: 0kg";
        }
    }
}
