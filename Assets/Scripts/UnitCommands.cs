using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCommands : MonoBehaviour
{
    [SerializeField] private UnitOrder selectedCommand = UnitOrder.Idle;

    //[SerializeField] private List<Toggle> commandButtons = new();

    void Start()
    {
        
    }

    public void SetCommand(int command)
    {
        // Check if no buttons are active
        /*bool buttonActive = false;
        foreach (var button in commandButtons)
        {
            if (button.isOn)
            {
                buttonActive = true;
            }
        }

        if (!buttonActive)
        {
            selectedCommand = UnitOrder.Idle;
            SendOrder(selectedCommand);
        }
        else
        {
            selectedCommand = (UnitOrder)command;
        }*/

        selectedCommand = (UnitOrder)command;
    }

    private void SendOrder(UnitOrder command)
    {
        Debug.Log(command + " order given");

        if (UnitManager.Instance.unitsSelected.Count == 0)
            return;

        foreach (DroneUnit unit in UnitManager.Instance.unitsSelected)
        {
            unit.Command(command);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedCommand == UnitOrder.Idle)
                return;

            SendOrder(selectedCommand);
        }
    }
}
