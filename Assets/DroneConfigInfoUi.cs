using Drone;
using TMPro;
using UnityEngine;

public class DroneConfigInfoUi : MonoBehaviour
{
    [SerializeField] private TMP_Text droneClassText;
    [SerializeField] private TMP_Text droneModsText;
    [SerializeField] private TMP_Text droneRangeText;
    [SerializeField] private TMP_Text droneSpeedText;
    [SerializeField] private TMP_Text droneWeightText;

    private void Start()
    {
        droneClassText.text = "DRONE CLASS: <color=white>NONE</color>";
        droneModsText.text = "MODS: <color=white>0</color>";
        droneRangeText.text = "RANGE: <color=white>0km</color>";
        droneSpeedText.text = "SPEED: <color=white>0mph</color>";
        droneWeightText.text = "WEIGHT: <color=white>0kg</color>";
    }

    private void Update()
    {
        // TODO: Clean
        if (DroneCarousel.Instance.DroneToBeEdited != null)
        {
            droneClassText.text = $"DRONE CLASS: <color=white>{DroneCarousel.Instance.DroneToBeEdited.DroneConfig.droneName}</color>";
            droneModsText.text = $"MODS: <color=white>{DroneCarousel.Instance.DroneToBeEdited.NumOfAttachments}</color>";
            droneRangeText.text = $"RANGE: <color=white>{DroneCarousel.Instance.DroneToBeEdited.Drone.Range}km</color>";
            droneSpeedText.text = $"SPEED: <color=white>{DroneCarousel.Instance.DroneToBeEdited.Drone.Speed}mph</color>";
            droneWeightText.text = $"WEIGHT: <color=white>{DroneCarousel.Instance.DroneToBeEdited.Drone.Weight}kg</color>";
        }
    }
}
