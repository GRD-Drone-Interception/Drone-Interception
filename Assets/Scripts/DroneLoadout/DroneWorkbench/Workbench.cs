using System;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout.DroneWorkbench
{
    /// <summary>
    /// Responsible for adding, removing, decorating, and resetting drone
    /// objects that have been added to the workbench. 
    /// </summary>
    public class Workbench : MonoBehaviour
    {
        public event Action<Drone> OnDroneBeingEditedChanged; 
        public Drone DroneBeingEdited => _droneBeingEdited;
        
        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Button addToFleetButton;
        [SerializeField] private Button editDroneButton;
        [SerializeField] private Button resetDroneConfigButton;
        private Drone _droneBeingEdited;
        private Player _player;

        private void Awake() => _player = FindObjectOfType<Player>();
        private void OnEnable()
        {
            addToFleetButton.onClick.AddListener(AddDroneToFleet);
            resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
        }

        private void OnDisable()
        {
            addToFleetButton.onClick.RemoveListener(AddDroneToFleet);
            resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
        }

        private void Start() => resetDroneConfigButton.gameObject.SetActive(false);

        private void Update()
        {
            // TODO: Call this on an event trigger. This shouldn't be in Update
            if (DroneLoadoutCameraMode.CurrentCameraMode == DroneLoadoutCameraMode.CameraMode.Display)
            {
                addToFleetButton.gameObject.SetActive(_droneBeingEdited != null);
                editDroneButton.gameObject.SetActive(_droneBeingEdited != null);
            }
        }

        private void AddToBench(Drone drone)
        {
            //_player.BuildBudget.DecreaseBudget(drone.DroneConfigData.Cost);
            _player.BuildBudget.DecreaseBudget(drone.DecorableDrone.Cost);
            drone.transform.SetParent(transform);
            _droneBeingEdited = drone;
            OnDroneBeingEditedChanged?.Invoke(drone);
        }

        private void RemoveFromBench(Drone drone)
        {
            //_player.BuildBudget.IncreaseBudget(drone.DroneConfigData.Cost);
            _player.BuildBudget.IncreaseBudget(drone.DecorableDrone.Cost);
            drone.transform.SetParent(null);
        }

        private void ResetCurrentDroneConfig()
        {
            if (_droneBeingEdited != null)
            {
                _droneBeingEdited.ResetConfiguration();
            }
        }
    
        private void DeleteCurrentDrone()
        {
            RemoveFromBench(_droneBeingEdited);
            Destroy(_droneBeingEdited.gameObject);
            OnDroneBeingEditedChanged?.Invoke(null);
        }

        public void SpawnDronePrefab(GameObject prefab)
        {
            if (_droneBeingEdited != null)
            {
                DeleteCurrentDrone();
            }

            var droneGameObject = Instantiate(prefab);
            droneGameObject.transform.position = droneSpawnPosition.position;
            droneGameObject.layer = LayerMask.NameToLayer("Focus");
            SetLayersRecursively(droneGameObject.transform);
            var drone = droneGameObject.GetComponent<Drone>();
            AddToBench(drone);
        }

        private void AddDroneToFleet()
        {
            Debug.Log($"{_droneBeingEdited.DroneConfigData.droneName} <color=green>added to fleet</color>");
            _player.DroneSwarm.AddToSwarm(_droneBeingEdited); 
            DeleteCurrentDrone();

            /*if (!_player.DroneSwarm.Drones.Contains(_droneBeingEdited))
            {
                Debug.Log($"{_droneBeingEdited.DroneConfigData.droneName} <color=green>added to fleet</color>");
                _player.DroneSwarm.AddToSwarm(_droneBeingEdited);
                DeleteCurrentDrone();
            }
            else
            {
                Debug.Log($"{_droneBeingEdited.DroneConfigData.droneName} <color=red>already in fleet</color>");
            }*/
        }

        private void SetLayersRecursively(Transform droneObject)
        {
            foreach (Transform child in droneObject)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Focus"); 
                // recursively call SetLayersRecursively for each child
                SetLayersRecursively(child);
            }
        }
    }
}
