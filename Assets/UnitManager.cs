using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    public List<DroneUnit> units = new List<DroneUnit>();
    public List<DroneUnit> unitsSelected = new List<DroneUnit>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning($"There should only be one instance of {Instance.GetType()}");
        }
    }

    public void ClickSelect(DroneUnit unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.selectedIcon.SetActive(true);
    }

    public void ShiftClickSelect(DroneUnit unitToAdd)
    {
        if(!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.selectedIcon.SetActive(true);
        }
        else
        {
            unitsSelected.Remove(unitToAdd);
            unitToAdd.selectedIcon.SetActive(false);
        }
    }

    public void DragSelect(DroneUnit unitToAdd)
    {
        if(!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.selectedIcon.SetActive(true);
        }
    }

    public void DeselectAll()
    {
        foreach (DroneUnit unit in unitsSelected)
        {
            unit.selectedIcon.SetActive(false);
        }
        unitsSelected.Clear();
    }

    public void Deselect(DroneUnit unitToAdd)
    {

    }
}
