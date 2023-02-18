using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroneSetupMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public event Action<DroneSetupMenuButton> OnButtonPressed;

    [SerializeField] private DroneSetupMenuStates menuState;
    private TMP_Text _text;
    private bool _isActive;

    private void Awake() => _text = GetComponent<TMP_Text>();

    public void OnPointerEnter(PointerEventData eventData) => SetButtonFontColour(new Color(0.54f, 0.73f, 1));

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isActive) { return; }
        SetButtonFontColour(Color.white);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isActive = true;
        SetButtonFontStyle(FontStyles.Underline | FontStyles.Bold);
        OnButtonPressed?.Invoke(this);
    }

    public void SetButtonFontColour(Color color) => _text.color = color;
    public void SetButtonFontStyle(FontStyles fontStyle) => _text.fontStyle = fontStyle;
    public void SetActive(bool active) => _isActive = active;
    public DroneSetupMenuStates GetMenuState() => menuState;
}
