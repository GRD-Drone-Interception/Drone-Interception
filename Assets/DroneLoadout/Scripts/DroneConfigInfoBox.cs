using DroneLoadout.DroneWorkbench;
using TMPro;
using UnityEngine;

namespace DroneLoadout.Scripts
{
    public class DroneConfigInfoBox : MonoBehaviour
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

        private void Update()
        {
            if (_workbench.DroneOnBench != null)
            {
                droneTypeText.text = $"DRONE TYPE: {_workbench.DroneOnBench.DroneConfigData.DroneType}";
                droneModelText.text = $"DRONE MODEL: {_workbench.DroneOnBench.DroneConfigData.DroneName}";
                droneModsText.text = $"MODS: {_workbench.DroneOnBench.NumOfMountedAttachments}";
                droneCostText.text = $"COST: {_workbench.DroneOnBench.DecorableDrone.Cost:C0}";
                droneRangeText.text = $"RANGE: {_workbench.DroneOnBench.DecorableDrone.Range}km";
                droneSpeedText.text = $"SPEED: {_workbench.DroneOnBench.DecorableDrone.TopSpeed}mph";
                droneAccelerationText.text = $"ACCELERATION: {_workbench.DroneOnBench.DecorableDrone.Acceleration}km/h in ?s";
                droneWeightText.text = $"WEIGHT: {_workbench.DroneOnBench.DecorableDrone.Weight}kg";
            }
        }
    }
}
