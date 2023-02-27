using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DroneLoadout
{
    public class DroneSetupMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public event Action<DroneSetupMenuButton> OnButtonPressed;

        [SerializeField] private DroneSetupMenuStates menuState;
        private TMP_Text _text;
        private bool _isActive;

        private void Awake() => _text = GetComponent<TMP_Text>();

        public void OnPointerEnter(PointerEventData eventData) => SetButtonFontColour(new Color(0, 1, 0.68F, 1));

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isActive) { return; }
            SetButtonFontColour(Color.black); 
            SetButtonFontStyle(FontStyles.Normal);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isActive = true;
            SetButtonFontStyle(FontStyles.Bold);
            OnButtonPressed?.Invoke(this);
        }

        public void SetButtonFontColour(Color color) => _text.color = color;
        public void SetButtonFontStyle(FontStyles fontStyle) => _text.fontStyle = fontStyle;
        public void SetActive(bool active) => _isActive = active;
        public DroneSetupMenuStates GetMenuState() => menuState;
    }
}
