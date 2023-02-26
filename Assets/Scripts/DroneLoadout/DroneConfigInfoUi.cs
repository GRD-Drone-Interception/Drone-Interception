using DroneLoadout.DroneWorkbench;
using TMPro;
using UnityEngine;

namespace DroneLoadout
{
    public class DroneConfigInfoUi : MonoBehaviour
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
        private void OnEnable() => _workbench.OnDroneOnBenchChanged += SubscribeToNewDronesDecoratedEvents;
        private void OnDisable() => _workbench.OnDroneOnBenchChanged -= SubscribeToNewDronesDecoratedEvents;

        private void SubscribeToNewDronesDecoratedEvents(Drone drone)
        {
            UpdateDroneInfoUi(drone, null);
            _workbench.DroneOnBench.OnDroneDecorationAdded += UpdateDroneInfoUi;
            _workbench.DroneOnBench.OnDroneDecorationRemoved += UpdateDroneInfoUi;
        }

        private void UpdateDroneInfoUi(Drone drone, DroneAttachment droneAttachment)
        {
            if (drone == null)
            {
                ClearDroneInfoFromUI();
                return;
            }
            
            droneTypeText.text = $"DRONE TYPE: <color=white>{drone.DroneConfigData.droneType}</color>";
            droneModelText.text = $"DRONE MODEL: <color=white>{drone.DroneConfigData.droneName}</color>";
            droneModsText.text = $"MODS: <color=white>{drone.NumOfMountedAttachments}</color>";
            droneCostText.text = $"COST: <color=white>{drone.DecorableDrone.Cost:C0}</color>";
            droneRangeText.text = $"RANGE: <color=white>{drone.DecorableDrone.Range}km</color>";
            droneSpeedText.text = $"SPEED: <color=white>{drone.DecorableDrone.Speed}mph</color>";
            droneAccelerationText.text = $"ACCELERATION: <color=white>{drone.DecorableDrone.Acceleration}km/h in ?s</color>";
            droneWeightText.text = $"WEIGHT: <color=white>{drone.DecorableDrone.Weight}kg</color>";
        }

        private void ClearDroneInfoFromUI()
        {
            droneTypeText.text = "DRONE TYPE: <color=white>NONE</color>";
            droneModelText.text = "DRONE MODEL: <color=white>NONE</color>";
            droneModsText.text = "MODS: <color=white>0</color>";
            droneCostText.text = "COST: <color=white>£0</color>";
            droneRangeText.text = "RANGE: <color=white>0km</color>";
            droneSpeedText.text = "SPEED: <color=white>0mph</color>";
            droneAccelerationText.text = "ACCELERATION: <color=white>km/h in ?s</color>";
            droneWeightText.text = "WEIGHT: <color=white>0kg</color>";
        }
    }
}
