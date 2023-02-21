using System;
using UnityEngine;
using UnityEngine.UI;

namespace Drones
{
    public class DroneTypeButton : MonoBehaviour
    {
        public event Action<DroneTypeButton> OnDroneTypeSelected;
        
        [SerializeField] private GameObject droneTypeModelSubMenuContainer;
        private Button _button; 

        private void OnEnable() => _button.onClick.AddListener(ShowModelSubMenu);
        private void OnDisable() => _button.onClick.RemoveListener(ShowModelSubMenu);
        private void Awake() => _button = GetComponent<Button>();
        private void Start() => droneTypeModelSubMenuContainer.SetActive(false);

        private void ShowModelSubMenu()
        {
            // hide drone attachment slot amd component sub menu
            foreach (var droneAttachmentSlot in FindObjectsOfType<DroneAttachmentSlot>())
            {
                droneAttachmentSlot.gameObject.SetActive(false);
                droneAttachmentSlot.HideComponentSubMenu();
            }
            
            droneTypeModelSubMenuContainer.SetActive(true);
            OnDroneTypeSelected?.Invoke(this);
        }

        public void HideModelSubMenu() => droneTypeModelSubMenuContainer.SetActive(false);
    }
}
