using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Drones
{
    public class DroneTypeSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<DroneTypeSelectButton> OnDroneTypeSelected;
        
        [SerializeField] private GameObject droneTypeSubMenuContainer;
        private Image _droneTypePanelImage;
        
        private void Awake() => _droneTypePanelImage = GetComponent<Image>();
        private void Start() => droneTypeSubMenuContainer.SetActive(false);

        public void OnPointerEnter(PointerEventData eventData) => Highlight();

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_droneTypePanelImage.raycastTarget)
            {
                NormalColour();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Highlight();
            ShowSubMenu();
            OnDroneTypeSelected?.Invoke(this);
        }

        public void Highlight() => _droneTypePanelImage.color = new Color(0.54f, 0.73f, 1, 0.1f);
        public void NormalColour() => _droneTypePanelImage.color = new Color(0, 0, 0, 0.8f);

        private void ShowSubMenu()
        {
            _droneTypePanelImage.raycastTarget = false;
            droneTypeSubMenuContainer.SetActive(true);
        }

        public void HideSubMenu()
        {
            _droneTypePanelImage.raycastTarget = true;
            droneTypeSubMenuContainer.SetActive(false);
        }
    }
}
