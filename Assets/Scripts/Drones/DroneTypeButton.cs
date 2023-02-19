using System;
using UnityEngine;
using UnityEngine.UI;

namespace Drones
{
    public class DroneTypeButton : MonoBehaviour
    {
        public event Action<DroneTypeButton> OnDroneTypeSelected;
        
        [SerializeField] private GameObject droneTypeSubMenuContainer;
        private Button _button; 

        private void OnEnable() => _button.onClick.AddListener(ShowSubMenu);
        private void OnDisable() => _button.onClick.RemoveListener(ShowSubMenu);
        private void Awake() => _button = GetComponent<Button>();
        private void Start() => droneTypeSubMenuContainer.SetActive(false);

        private void ShowSubMenu()
        {
            droneTypeSubMenuContainer.SetActive(true);
            OnDroneTypeSelected?.Invoke(this);
        }

        public void HideSubMenu() => droneTypeSubMenuContainer.SetActive(false);
    }
}
