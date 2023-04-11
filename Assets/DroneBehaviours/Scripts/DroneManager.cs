using System.Collections.Generic;
using DroneLoadout;
using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    public class DroneManager : MonoBehaviour
    {
        public static DroneManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                Debug.LogWarning($"There should only be one instance of {this}");
            }
        }

        public static List<Drone> ActiveDrones => _activeActiveDrones;
        private static List<Drone> _activeActiveDrones = new();

        public static void AddDrone(Drone drone)
        {
            _activeActiveDrones.Add(drone);
        }

        public static void RemoveDrone(Drone drone)
        {
            _activeActiveDrones.Remove(drone);
        }
        
        
        
        
        
        public List<Drone> unitsSelected = new List<Drone>();
        
        /// <summary>
        /// Selects unit on click
        /// </summary>
        /// <param name="unitClicked">The droneUnit component of the hit collider</param>
        public void ClickSelect(Drone unitClicked)
        {
            DeselectAll();
            Select(unitClicked);
        }
    
        /// <summary>
        /// Selects or deselects unit when shift clicked on
        /// </summary>
        /// <param name="unitClicked">The droneUnit component of the hit collider</param>
        public void ShiftClickSelect(Drone unitClicked)
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
        public void DragSelect(Drone unitToAdd)
        {
            if(!unitsSelected.Contains(unitToAdd))
            {
                unitsSelected.Add(unitToAdd);
                //unitToAdd.SetSelectionIcon(true);
                unitToAdd.Paint(Color.green);
                //unitToAdd.ApplyOutlineEffect();
            }
        }
    
        /// <summary>
        /// Deselects all currently selected units
        /// </summary>
        public void DeselectAll()
        {
            foreach (Drone unit in unitsSelected)
            {
                //unit.SetSelectionIcon(false);
                unit.ResetPaintJob();
                //unit.RemoveOutlineEffect();
            }
            unitsSelected.Clear();
        }
    
        /// <summary>
        /// Select an individual unit
        /// </summary>
        /// <param name="unitToAdd">The droneUnit component of the hit collider</param>
        private void Select(Drone unitToAdd)
        {
            unitsSelected.Add(unitToAdd);
            //unitToAdd.SetSelectionIcon(true);
            unitToAdd.Paint(Color.green);
            //unitToAdd.ApplyOutlineEffect();
        }
    
        /// <summary>
        /// Deselects an individual unit
        /// </summary>
        /// <param name="unitToRemove">The droneUnit component of the hit collider</param>
        private void Deselect(Drone unitToRemove)
        {
            unitsSelected.Remove(unitToRemove);
            //unitToRemove.SetSelectionIcon(false);
            unitToRemove.ResetPaintJob();
            //unitToRemove.RemoveOutlineEffect();
        }
    }
}
