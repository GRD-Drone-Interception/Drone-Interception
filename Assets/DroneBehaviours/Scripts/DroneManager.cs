using System.Collections.Generic;
using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    public class DroneManager : MonoBehaviour
    {
        public static DroneManager Instance { get; private set; }
        public List<Drone> Drones { get; } = new();
        public List<Drone> SelectedDrones { get; } = new();

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

        public void AddDrone(Drone drone)
        {
            Drones.Add(drone);
        }

        public void RemoveDrone(Drone drone)
        {
            Drones.Remove(drone);
        }

        public void Move(Vector3 targetDestination)
        {
            foreach (var drone in SelectedDrones)
            {
                drone.Move(targetDestination);
            }
        }

        /// <summary>
        /// Selects unit on click
        /// </summary>
        /// <param name="unitClicked">The droneUnit component of the hit collider</param>
        public void ClickSelect(Drone unitClicked)
        {
            //DeselectAll();
            if (Drones.Contains(unitClicked))
            {
                Select(unitClicked);
            }
        }
    
        /// <summary>
        /// Selects or deselects unit when shift clicked on
        /// </summary>
        /// <param name="unitClicked">The droneUnit component of the hit collider</param>
        public void ShiftClickSelect(Drone unitClicked)
        {
            if (!Drones.Contains(unitClicked))
            {
                return;
            }

            if(!SelectedDrones.Contains(unitClicked))
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
            if(!SelectedDrones.Contains(unitToAdd))
            {
                SelectedDrones.Add(unitToAdd);
                unitToAdd.Select();
            }
        }
    
        /// <summary>
        /// Deselects all currently selected units
        /// </summary>
        public void DeselectAll()
        {
            foreach (Drone unit in SelectedDrones)
            {
                unit.Unselect();
            }
            SelectedDrones.Clear();
        }
    
        /// <summary>
        /// Select an individual unit
        /// </summary>
        /// <param name="unitToAdd">The droneUnit component of the hit collider</param>
        private void Select(Drone unitToAdd)
        {
            SelectedDrones.Add(unitToAdd);
            unitToAdd.Select();
        }
    
        /// <summary>
        /// Deselects an individual unit
        /// </summary>
        /// <param name="unitToRemove">The droneUnit component of the hit collider</param>
        public void Deselect(Drone unitToRemove)
        {
            SelectedDrones.Remove(unitToRemove);
            unitToRemove.Unselect();
        }
    }
}
