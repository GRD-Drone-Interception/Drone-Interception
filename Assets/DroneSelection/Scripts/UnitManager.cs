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

    /// <summary>
    /// Selects unit on click
    /// </summary>
    /// <param name="unitClicked">The droneUnit component of the hit collider</param>
    public void ClickSelect(DroneUnit unitClicked)
    {
        DeselectAll();
        Select(unitClicked);
    }

    /// <summary>
    /// Selects or deselects unit when shift clicked on
    /// </summary>
    /// <param name="unitClicked">The droneUnit component of the hit collider</param>
    public void ShiftClickSelect(DroneUnit unitClicked)
    {
        if(!unitsSelected.Contains(unitClicked))
        {
            Select(unitClicked);
        }
        else
        {
            Deselect(unitClicked);
        }
    }

    /// <summary>
    /// Selects units when screen dragged
    /// </summary>
    /// <param name="unitToAdd">The droneUnit component of the hit collider</param>
    public void DragSelect(DroneUnit unitToAdd)
    {
        if(!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.SetSelectionIcon(true);
        }
    }

    /// <summary>
    /// Deselects all currently selected units
    /// </summary>
    public void DeselectAll()
    {
        foreach (DroneUnit unit in unitsSelected)
        {
            unit.SetSelectionIcon(false);
        }
        unitsSelected.Clear();
    }

    /// <summary>
    /// Select an individual unit
    /// </summary>
    /// <param name="unitToAdd">The droneUnit component of the hit collider</param>
    public void Select(DroneUnit unitToAdd)
    {
        unitsSelected.Add(unitToAdd);
        unitToAdd.SetSelectionIcon(true);
    }

    /// <summary>
    /// Deselects an individual unit
    /// </summary>
    /// <param name="unitToRemove">The droneUnit component of the hit collider</param>
    public void Deselect(DroneUnit unitToRemove)
    {
        unitsSelected.Remove(unitToRemove);
        unitToRemove.SetSelectionIcon(false);
    }
}
