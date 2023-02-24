using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DroneLoadout
{
    public class DroneSubMenuManager : MonoBehaviour
    {
        private readonly List<DroneTypeButton> _droneTypeButtons = new();

        private void Awake() => _droneTypeButtons.AddRange(FindObjectsOfType<DroneTypeButton>());
        private void OnEnable() => _droneTypeButtons.ForEach(button => button.OnDroneTypeSelected += OnDroneTypeSelected);
        private void OnDisable() => _droneTypeButtons.ForEach(button => button.OnDroneTypeSelected -= OnDroneTypeSelected);

        private void OnDroneTypeSelected(DroneTypeButton buttonSelected)
        {
            foreach (var button in _droneTypeButtons.Where(button => button != buttonSelected))
            {
                button.HideModelSubMenu();
            }
        }
    }
}
