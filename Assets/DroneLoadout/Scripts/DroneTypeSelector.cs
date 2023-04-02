using System;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for displaying a list of drone models for the
    /// selected drone type.
    /// </summary>
    public class DroneTypeSelector : MonoBehaviour
    {
        public event Action<DroneTypeSelector> OnDroneTypeSelected;
        
        [SerializeField] private GameObject droneTypeModelSubMenuContainer;
        private Button _button; 

        private void OnEnable() => _button.onClick.AddListener(ShowModelSubMenu);
        private void OnDisable() => _button.onClick.RemoveListener(ShowModelSubMenu);
        private void Awake() => _button = GetComponent<Button>();
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

        public void HideModelSubMenu() => droneTypeModelSubMenuContainer.SetActive(false);
    }
}
