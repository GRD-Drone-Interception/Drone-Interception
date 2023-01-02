using System.Collections.Generic;
using Drone;
using PodiumNodes;
using UnityEngine;
using UnityEngine.UI;

public class Workbench : MonoBehaviour
{
    public InterceptorDrone DroneBeingEdited => _droneBeingEdited;
    public Transform DroneSpawnpoint => droneSpawnpoint;
    public WorkbenchCarousel Carousel => workbenchCarousel;
    public Dictionary<InterceptorDrone, PodiumNode> DronesOnPodiumDict => _dronesOnPodiumDict;
    
    [SerializeField] private WorkbenchCarousel workbenchCarousel;
    [SerializeField] private Transform droneSpawnpoint;
    [SerializeField] private Button resetDroneConfigButton;
    [SerializeField] private Button deleteDroneButton;
    private InterceptorDrone _droneBeingEdited;
    private Dictionary<InterceptorDrone, PodiumNode> _dronesOnPodiumDict = new();

    private void Start() => resetDroneConfigButton.gameObject.SetActive(false);

    private void OnEnable()
    {
        resetDroneConfigButton.onClick.AddListener(ResetCurrentDroneConfig);
        deleteDroneButton.onClick.AddListener(DeleteCurrentDrone);
        DroneDetector.OnDroneDetected += delegate(InterceptorDrone drone) { _droneBeingEdited = drone; };
        DroneDetector.OnDroneDetectionExit += delegate { _droneBeingEdited = null; };
    }

    private void OnDisable()
    {
        resetDroneConfigButton.onClick.RemoveListener(ResetCurrentDroneConfig);
        deleteDroneButton.onClick.RemoveListener(DeleteCurrentDrone);
        DroneDetector.OnDroneDetected -= delegate(InterceptorDrone drone) { _droneBeingEdited = drone; };
        DroneDetector.OnDroneDetectionExit -= delegate { _droneBeingEdited = null; };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<InterceptorDrone>() != null)
        {
            var drone = collision.transform.GetComponent<InterceptorDrone>();
            Destroy(collision.transform.GetComponent<Rigidbody>());
            AddToBench(drone, workbenchCarousel.PodiumNodes[workbenchCarousel.CurrentPodiumNodeIndex]);
        }
    }

    private void AddToBench(InterceptorDrone drone, PodiumNode node)
    {
        drone.transform.SetParent(workbenchCarousel.transform);
        _dronesOnPodiumDict.Add(drone, node);
        FindObjectOfType<Player>().DroneSwarm.AddToSwarm(drone); // hm
    }

    private void RemoveFromBench(InterceptorDrone drone)
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
