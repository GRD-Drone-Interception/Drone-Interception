using System.Collections.Generic;
using System.Linq;
using Drones;
using UnityEngine;

public class DroneSubMenuManager : MonoBehaviour
{
    [SerializeField] private List<DroneTypeButton> droneTypeButtons;

    private void OnEnable() => droneTypeButtons.ForEach(button => button.OnDroneTypeSelected += OnDroneTypeSelected);
    private void OnDisable() => droneTypeButtons.ForEach(button => button.OnDroneTypeSelected -= OnDroneTypeSelected);

    private void OnDroneTypeSelected(DroneTypeButton buttonSelected)
    {
        foreach (var button in droneTypeButtons.Where(button => button != buttonSelected))
        {
            button.HideModelSubMenu();
        }
    }
}
