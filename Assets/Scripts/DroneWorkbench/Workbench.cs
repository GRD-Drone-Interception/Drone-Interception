using Drones;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace DroneWorkbench
{
    public class Workbench : MonoBehaviour
    {
        public Drone DroneBeingEdited => _droneBeingEdited;
        public Transform DroneSpawnPoint => droneSpawnPosition;

        [SerializeField] private Transform droneSpawnPosition;
        [SerializeField] private Button resetDroneConfigButton;
        [SerializeField] private Button deleteDroneButton;
        private Drone _droneBeingEdited;

        private void OnEnable()
        {
            resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
            deleteDroneButton.onClick.AddListener(DeleteCurrentDrone);
        }

        private void OnDisable()
        {
            resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
            deleteDroneButton.onClick.RemoveListener(DeleteCurrentDrone);
        }
        
        private void Start()
        {
            resetDroneConfigButton.gameObject.SetActive(false);
        }

        public void AddToBench(Drone drone, Node node)
        {
            drone.transform.SetParent(transform);
            //_dronesOnPodiumDict.Add(drone, node);
            //FindObjectOfType<Player>().DroneSwarm.AddToSwarm(drone); // hm
        }

        private void RemoveFromBench(Drone drone)
        {
            drone.transform.SetParent(null);
            //_dronesOnPodiumDict.Remove(_droneBeingEdited);
            //FindObjectOfType<Player>().DroneSwarm.RemoveFromSwarm(drone); // hm
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
        }
    }
}
