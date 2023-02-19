using System;
using DroneWorkbench;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Drones
{
    /// <summary>
    /// Used to instantiate a drone on the workbench when a drone spawn
    /// button is pressed. 
    /// </summary>
    public class DroneCreator : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnDroneSpawned;
        
        [SerializeField] private GameObject droneTypePrefab;
        private Workbench _workbench; 

        private void Awake() => _workbench = FindObjectOfType<Workbench>();

        public void OnPointerDown(PointerEventData eventData)
        { 
            SpawnDroneClass();
        }

        private void SpawnDroneClass()
        {
            var drone = Instantiate(droneTypePrefab);
            drone.transform.position = _workbench.DroneSpawnPoint.position;
            OnDroneSpawned?.Invoke();
        }
    }
}