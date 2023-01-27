using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsUI : MonoBehaviour
{
    [SerializeField] private GameObject unitsContainer;
    [SerializeField] private GameObject unitUIPrefab;

    [SerializeField] private List<DroneUnit> units = new List<DroneUnit>();

    void Start()
    {
        foreach (DroneUnit unit in units)
        {
            AddDroneToUnits(unit);
        }
    }

    public void AddDroneToUnits(DroneUnit unit)
    {
        GameObject ui = Instantiate(unitUIPrefab, unitsContainer.transform);

        UnitUI unitUI = ui.GetComponent<UnitUI>();
        unitUI.SetUnit(unit);
        unit.ui = unitUI;
    }

    void Update()
    {
        
    }
}
