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
        private DroneWorkshop.DroneWorkbench _droneWorkbench;

        private void Awake() => _droneWorkbench = FindObjectOfType<DroneWorkshop.DroneWorkbench>();

        private void Update()
        {
            if (_droneWorkbench.DroneOnBench != null)
            {
                droneTypeText.text = $"DRONE TYPE: {_droneWorkbench.DroneOnBench.GetDroneType()}";
                droneModelText.text = $"DRONE MODEL: {_droneWorkbench.DroneOnBench.GetName()}";
                droneModsText.text = $"MODS: {_droneWorkbench.DroneOnBench.NumOfMountedAttachments}";
                droneCostText.text = $"COST: {_droneWorkbench.DroneOnBench.DecorableDrone.Cost:C0}";
                droneRangeText.text = $"RANGE: {_droneWorkbench.DroneOnBench.DecorableDrone.Range}km";
                droneSpeedText.text = $"TOP SPEED: {_droneWorkbench.DroneOnBench.DecorableDrone.TopSpeed}mph";
                droneAccelerationText.text = $"ACCELERATION: {_droneWorkbench.DroneOnBench.DecorableDrone.Acceleration}km/h in ?s";
                droneWeightText.text = $"WEIGHT: {_droneWorkbench.DroneOnBench.DecorableDrone.Weight}kg";
            }
        }
    }
}
