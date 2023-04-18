using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DroneLoadout.Scripts
{
    /// <summary>
    /// Responsible for displaying a list of drone models for the
    /// selected drone type.
    /// </summary>
    public class DroneTypeSelector : MonoBehaviour
    {
        public event Action<DroneTypeSelector> OnDroneTypeSelected;
        
        [SerializeField] private GameObject droneTypeModelSubMenuContainer;
        private Button _typeButton;

        private void OnEnable() => _typeButton.onClick.AddListener(ShowModelSubMenu);
        private void OnDisable() => _typeButton.onClick.RemoveListener(ShowModelSubMenu);
        private void Awake() => _typeButton = GetComponent<Button>();
        private void Start() => droneTypeModelSubMenuContainer.SetActive(false);

        private void ShowModelSubMenu()
        {
            // If the drone type sub-menu isn't already displayed, display it
            if (!droneTypeModelSubMenuContainer.activeSelf)
            {
                droneTypeModelSubMenuContainer.SetActive(true);
                OnDroneTypeSelected?.Invoke(this);
            }
            else
            {
                HideModelSubMenu();
            }
        }

        public void HideModelSubMenu()
        {
            droneTypeModelSubMenuContainer.SetActive(false);
        }
    }
}
