using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DroneLoadout.Scripts
{
    public class DroneTypeSelectorDisplayer : MonoBehaviour
    {
        private readonly List<DroneTypeSelector> _droneTypeButtons = new();

        private void Awake() => _droneTypeButtons.AddRange(FindObjectsOfType<DroneTypeSelector>());
        
        private void OnEnable()
        {
            WorkshopModeController.OnModeChange += SetVisibilityOfAttachmentSlotsOnModeChange;
            _droneTypeButtons.ForEach(button => button.OnDroneTypeSelected += HideDroneTypeButtonsOnTypeSelected);
        }

        private void OnDisable()
        {
            WorkshopModeController.OnModeChange -= SetVisibilityOfAttachmentSlotsOnModeChange;
            _droneTypeButtons.ForEach(button => button.OnDroneTypeSelected -= HideDroneTypeButtonsOnTypeSelected);
        }

        private void SetVisibilityOfAttachmentSlotsOnModeChange(WorkshopModeController.WorkshopMode mode)
        {
            switch (mode)
            {
                case WorkshopModeController.WorkshopMode.Edit:
                    _droneTypeButtons.ForEach(slot => slot.gameObject.SetActive(false));
                    _droneTypeButtons.ForEach(slot => slot.HideModelSubMenu());
                    break;
                case WorkshopModeController.WorkshopMode.Display:
                    _droneTypeButtons.ForEach(slot => slot.gameObject.SetActive(true));
                    break;
            }
        }
        
        private void HideDroneTypeButtonsOnTypeSelected(DroneTypeSelector selectorSelected)
        {
            // Hide drone type sub-menus besides the one currently selected
            foreach (var button in _droneTypeButtons.Where(button => button != selectorSelected))
            {
                button.HideModelSubMenu();
            }
        }
    }
}