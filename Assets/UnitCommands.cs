using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommands : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnMoveOrder()
    {
        SendOrder(UnitOrder.Move);
    }

    public void OnAttackOrder()
    {
        SendOrder(UnitOrder.Attack);
    }

    public void OnDefendOrder()
    {
        SendOrder(UnitOrder.Defend);
    }

    public void OnReconOrder()
    {
        SendOrder(UnitOrder.Recon);
    }

    private void SendOrder(UnitOrder command)
    {
        Debug.Log(command + " order given");

        if (UnitManager.Instance.unitsSelected.Count == 0)
            return;

        foreach (DroneUnit unit in UnitManager.Instance.unitsSelected)
        {
            switch (command)
            {
                case UnitOrder.Move:
                    unit.Move();
                    break;
                case UnitOrder.Recon:
                    unit.Recon();
                    break;
                case UnitOrder.Attack:
                    unit.Attack();
                    break;
                case UnitOrder.Defend:
                    unit.Defend();
                    break;
                default:
                    break;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SendOrder(UnitOrder.Move);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SendOrder(UnitOrder.Attack);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SendOrder(UnitOrder.Defend);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SendOrder(UnitOrder.Recon);
        }
    }
}
