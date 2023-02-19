using System;
using DroneWorkbench;
using TMPro;
using UnityEngine;

namespace Drones
{
    public class DroneConfigInfoUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text droneClassText;
        [SerializeField] private TMP_Text droneModsText;
        [SerializeField] private TMP_Text droneRangeText;
        [SerializeField] private TMP_Text droneSpeedText;
        [SerializeField] private TMP_Text droneWeightText;
        [SerializeField] private Workbench _workbench;

        private void OnEnable() => _workbench.OnDroneSpawned += UpdateDroneInfoUi;
        private void OnDisable() => _workbench.OnDroneSpawned -= UpdateDroneInfoUi;

        private void UpdateDroneInfoUi(Drone drone)
        {
            Debug.Log(drone);
            droneClassText.text = $"DRONE CLASS: <color=white>{drone.DroneConfigSo.droneName}</color>";
            droneModsText.text = $"MODS: <color=white>{drone.NumOfAttachments}</color>";
            droneRangeText.text = $"RANGE: <color=white>{drone.DecorableDrone.Range}km</color>";
            droneSpeedText.text = $"SPEED: <color=white>{drone.DecorableDrone.Speed}mph</color>";
            droneWeightText.text = $"WEIGHT: <color=white>{drone.DecorableDrone.Weight}kg</color>";
        }

        private void ResetDroneInfoUiToDefault()
        {
            droneClassText.text = "DRONE CLASS: <color=white>NONE</color>";
            droneModsText.text = "MODS: <color=white>0</color>";
            droneRangeText.text = "RANGE: <color=white>0km</color>";
            droneSpeedText.text = "SPEED: <color=white>0mph</color>";
            droneWeightText.text = "WEIGHT: <color=white>0kg</color>";
        }
    }
}
