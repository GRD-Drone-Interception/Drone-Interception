using System.Collections.Generic;
using System.Linq;
using Drones;
using UnityEngine;

public class DroneTypeButtons : MonoBehaviour
{
    [SerializeField] private List<DroneTypeSelectButton> droneTypeButtons;

    private void OnEnable() => droneTypeButtons.ForEach(button => button.OnDroneTypeSelected += OnDroneTypeSelected);
    private void OnDisable() => droneTypeButtons.ForEach(button => button.OnDroneTypeSelected -= OnDroneTypeSelected);

    private void OnDroneTypeSelected(DroneTypeSelectButton buttonSelected)
    {
        buttonSelected.Highlight();

        foreach (var button in droneTypeButtons.Where(button => button != buttonSelected))
        {
            button.NormalColour();
            button.HideSubMenu();
        }
    }
}
