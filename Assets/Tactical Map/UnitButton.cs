using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButton : MonoBehaviour
{
    public int ID { get; private set; }

    public void SetID(int id)
    {
        ID = id;
    }

    public void SelectUnit()
    {
        TacticalMap.Instance.SelectUnit(ID);
    }
}
