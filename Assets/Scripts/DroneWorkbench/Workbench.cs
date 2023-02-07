using System.Collections.Generic;
using Drones;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace DroneWorkbench
{
    public class Workbench : MonoBehaviour
    {
        public Drone DroneBeingEdited => _droneBeingEdited;
        public WorkbenchCarousel Carousel => workbenchCarousel;
        public Dictionary<Drone, Node> DronesOnPodiumDict => _dronesOnPodiumDict;
    
        [SerializeField] private WorkbenchCarousel workbenchCarousel;
        [SerializeField] private Button resetDroneConfigButton;
        [SerializeField] private Button deleteDroneButton;
        private Drone _droneBeingEdited;
        private Dictionary<Drone, Node> _dronesOnPodiumDict = new();

        private void OnEnable()
        {
            resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
            deleteDroneButton.onClick.AddListener(DeleteCurrentDrone);
            //DroneDetector.OnDroneDetected += delegate(Drone drone) { _droneBeingEdited = drone; };
            //DroneDetector.OnDroneDetectionExit += delegate { _droneBeingEdited = null; };
        }

        private void OnDisable()
        {
            resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
            deleteDroneButton.onClick.RemoveListener(DeleteCurrentDrone);
            //DroneDetector.OnDroneDetected -= delegate(Drone drone) { _droneBeingEdited = drone; };
            //DroneDetector.OnDroneDetectionExit -= delegate { _droneBeingEdited = null; };
        }
        
        private void Start()
        {
            resetDroneConfigButton.gameObject.SetActive(false);
            //_droneBeingEdited = _dronesOnPodiumDict.Keys.GetEnumerator().Current;
            //_droneBeingEdited = drone at node 0 on carousel
        }

        /*private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<Drone>() != null)
            {
                var drone = collision.transform.GetComponent<Drone>();
                Destroy(collision.transform.GetComponent<Rigidbody>());
                AddToBench(drone, workbenchCarousel.Nodes[workbenchCarousel.CurrentPodiumNodeIndex]);
            }
        }*/

        public void AddToBench(Drone drone, Node node)
        {
            drone.transform.SetParent(workbenchCarousel.transform);
            _dronesOnPodiumDict.Add(drone, node);
            FindObjectOfType<Player>().DroneSwarm.AddToSwarm(drone); // hm
        }

        private void RemoveFromBench(Drone drone)
        {
            drone.transform.SetParent(null);
            _dronesOnPodiumDict.Remove(_droneBeingEdited);
            FindObjectOfType<Player>().DroneSwarm.RemoveFromSwarm(drone); // hm
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
