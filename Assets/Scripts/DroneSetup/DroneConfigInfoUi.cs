using DroneSetup.DroneWorkbench;
using TMPro;
using UnityEngine;

namespace DroneSetup
{
    public class DroneConfigInfoUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text droneTypeText;
        [SerializeField] private TMP_Text droneModelText;
        [SerializeField] private TMP_Text droneModsText;
        [SerializeField] private TMP_Text droneRangeText;
        [SerializeField] private TMP_Text droneSpeedText;
        [SerializeField] private TMP_Text droneAccelerationText;
        [SerializeField] private TMP_Text droneWeightText;
        private Workbench _workbench;

        private void Awake() => _workbench = FindObjectOfType<Workbench>();

        private void OnEnable() => _workbench.OnDroneBeingEditedChanged += SubscribeToNewDronesDecoratedEvents;

        private void OnDisable() => _workbench.OnDroneBeingEditedChanged -= SubscribeToNewDronesDecoratedEvents;

        private void SubscribeToNewDronesDecoratedEvents(Drone drone)
        {
            UpdateDroneInfoUi(drone);
            _workbench.DroneBeingEdited.OnDroneDecorated += UpdateDroneInfoUi;
        }

        private void UpdateDroneInfoUi(Drone drone)
        {
            if (drone == null)
            {
                ClearDroneInfoFromUI();
                return;
            }
            
            droneTypeText.text = $"DRONE TYPE: <color=white>{drone.DroneConfigSo.droneType}</color>";
            droneModelText.text = $"DRONE MODEL: <color=white>{drone.DroneConfigSo.droneName}</color>";
            droneModsText.text = $"MODS: <color=white>{drone.NumOfAttachments}</color>";
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
            droneRangeText.text = "RANGE: <color=white>0km</color>";
            droneSpeedText.text = "SPEED: <color=white>0mph</color>";
            droneAccelerationText.text = "ACCELERATION: <color=white>km/h in ?s</color>";
            droneWeightText.text = "WEIGHT: <color=white>0kg</color>";
        }
    }
}
