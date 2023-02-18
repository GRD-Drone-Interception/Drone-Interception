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
        
        [SerializeField] private GameObject prefabToSpawn;
        private Workbench _workbench; // Unnecessary dependency

        private void Awake() => _workbench = FindObjectOfType<Workbench>();

        public void OnPointerDown(PointerEventData eventData)
        { 
            SpawnDrone();
        }

        private void SpawnDrone()
        {
            var drone = Instantiate(prefabToSpawn);
            drone.transform.position = _workbench.DroneSpawnPoint.position;
            OnDroneSpawned?.Invoke();
        }
    }
}