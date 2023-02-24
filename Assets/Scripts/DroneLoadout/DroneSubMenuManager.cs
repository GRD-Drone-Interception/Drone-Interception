using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DroneLoadout
{
    public class DroneSubMenuManager : MonoBehaviour
    {
        private readonly List<DroneTypeSelector> _droneTypeButtons = new();

        private void Awake() => _droneTypeButtons.AddRange(FindObjectsOfType<DroneTypeSelector>());
        private void OnEnable() => _droneTypeButtons.ForEach(button => button.OnDroneTypeSelected += OnDroneTypeSelected);
        private void OnDisable() => _droneTypeButtons.ForEach(button => button.OnDroneTypeSelected -= OnDroneTypeSelected);

        private void OnDroneTypeSelected(DroneTypeSelector selectorSelected)
        {
            foreach (var button in _droneTypeButtons.Where(button => button != selectorSelected))
            {
                button.HideModelSubMenu();
            }
        }
    }
}
