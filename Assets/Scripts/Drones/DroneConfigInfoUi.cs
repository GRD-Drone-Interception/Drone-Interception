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
    
        private void OnEnable()
        {
            //DroneDetector.OnDroneDetected += UpdateDroneInfoUi;
            //DroneDetector.OnDroneDetectionExit += ResetDroneInfoUiToDefault;
            //FindObjectOfType<Workbench>().DroneBeingEdited.OnDroneDecorated += UpdateDroneInfoUi;
            /*foreach (var drone in FindObjectOfType<Workbench>().Drones)
        {
            drone.OnDroneDecorated += UpdateDroneInfoUi;
        }*/
        }

        private void OnDisable()
        {
            //DroneDetector.OnDroneDetected -= UpdateDroneInfoUi;
            //DroneDetector.OnDroneDetectionExit -= ResetDroneInfoUiToDefault;
            //FindObjectOfType<Workbench>().DroneBeingEdited.OnDroneDecorated -= UpdateDroneInfoUi;
            /*foreach (var drone in FindObjectOfType<Workbench>().Drones)
        {
            drone.OnDroneDecorated -= UpdateDroneInfoUi;
        }*/
        }

        private void Start() => ResetDroneInfoUiToDefault();

        private void Update()
        {
            if (FindObjectOfType<Workbench>().DroneBeingEdited != null) // TODO: Clean-up needed
            {
                UpdateDroneInfoUi(FindObjectOfType<Workbench>().DroneBeingEdited);
            }
            else
            { 
                ResetDroneInfoUiToDefault();
            }
        }

        private void UpdateDroneInfoUi(Drone drone)
        {
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
