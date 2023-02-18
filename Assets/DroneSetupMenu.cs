using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DroneSetupMenu : MonoBehaviour
{
    public DroneSetupMenuStates State { get; private set; }

    [SerializeField] private List<GameObject> menuUiContainers;
    private Dictionary<DroneSetupMenuStates, GameObject> _droneSetupMenuDictionary = new();
    private List<DroneSetupMenuButton> _subMenuButtons = new();
    private DroneSetupMenuButton _menuButton;

    private void Awake() => _subMenuButtons.AddRange(GetComponentsInChildren<DroneSetupMenuButton>());

    private void Start()
    {
        _menuButton = _subMenuButtons.FirstOrDefault(button => button.GetMenuState() == DroneSetupMenuStates.Lobby);
        _menuButton.SetButtonFontColour(new Color(0.54f, 0.73f, 1));
        _menuButton.SetButtonFontStyle(FontStyles.Underline | FontStyles.Bold);

        var menuStates = Enum.GetValues(typeof(DroneSetupMenuStates));
        for (int i = 0; i < menuStates.Length; i++)
        {
            _droneSetupMenuDictionary.Add((DroneSetupMenuStates)menuStates.GetValue(i), menuUiContainers[i]);
        }

        menuUiContainers.ForEach(menu => menu.SetActive(false));
        _droneSetupMenuDictionary[State].SetActive(true);
    }

    private void OnEnable() => _subMenuButtons.ForEach(button => button.OnButtonPressed += UpdateButtonFontStyles);
    private void OnDisable() => _subMenuButtons.ForEach(button => button.OnButtonPressed -= UpdateButtonFontStyles);

    private void UpdateButtonFontStyles(DroneSetupMenuButton newActiveMenuButton)
    {
        if (_menuButton != null && _menuButton != newActiveMenuButton)
        {
            // Reset font state and style of previously pressed button
            _menuButton.SetActive(false);
            _menuButton.SetButtonFontColour(Color.white);
            _menuButton.SetButtonFontStyle(FontStyles.Bold);
            _droneSetupMenuDictionary[State].SetActive(false);
        }
        _menuButton = newActiveMenuButton;
        State = newActiveMenuButton.GetMenuState();
        _droneSetupMenuDictionary[State].SetActive(true);
    }
}
