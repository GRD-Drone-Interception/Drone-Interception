using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    public int ID { get; private set; }

    public void SetID(int id)
    {
        ID = id;
    }

    public void OnUnitButtonPressed(bool active)
    {
        if(active)
        {
            SelectUnit(ID);
        }
        else
        {
            SelectUnit(-1);
        }

    }

    private void SelectUnit(int _ID)
    {
        TacticalMap.Instance.SelectUnit(_ID);
    }
}
