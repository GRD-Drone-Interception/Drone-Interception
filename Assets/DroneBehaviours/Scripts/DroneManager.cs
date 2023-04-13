using System.Collections.Generic;
using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    public class DroneManager : MonoBehaviour
    {
        public static DroneManager Instance { get; private set; }
        public static List<Drone> Drones { get; } = new();

        private readonly List<Drone> _unitsSelected = new();

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

        public static void AddDrone(Drone drone)
        {
            Drones.Add(drone);
        }

        public static void RemoveDrone(Drone drone)
        {
            Drones.Remove(drone);
        }

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
            if(!_unitsSelected.Contains(unitClicked))
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
            if(!_unitsSelected.Contains(unitToAdd))
            {
                _unitsSelected.Add(unitToAdd);
                unitToAdd.Select();
            }
        }
    
        /// <summary>
        /// Deselects all currently selected units
        /// </summary>
        public void DeselectAll()
        {
            foreach (Drone unit in _unitsSelected)
            {
                unit.Unselect();
            }
            _unitsSelected.Clear();
        }
    
        /// <summary>
        /// Select an individual unit
        /// </summary>
        /// <param name="unitToAdd">The droneUnit component of the hit collider</param>
        private void Select(Drone unitToAdd)
        {
            _unitsSelected.Add(unitToAdd);
            unitToAdd.Select();
        }
    
        /// <summary>
        /// Deselects an individual unit
        /// </summary>
        /// <param name="unitToRemove">The droneUnit component of the hit collider</param>
        public void Deselect(Drone unitToRemove)
        {
            _unitsSelected.Remove(unitToRemove);
            unitToRemove.Unselect();
        }
    }
}
