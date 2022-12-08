using UnityEngine;
using UnityEngine.EventSystems;

namespace Drone
{
    /// <summary>
    /// Responsible for spawning a drone of a given type onto the drone podium
    /// </summary>
    public class DroneCreator : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject prefabToSpawn;

        public void OnPointerDown(PointerEventData eventData)
        {
            var podiumPos = PodiumDetector.ActivePodium.transform.position;
            
            if(Physics.Raycast(podiumPos + Vector3.up, Vector3.down, out RaycastHit hitInfo))
            {
                var hitPodium = hitInfo.transform.GetComponent<Podium>();
                
                // Spawn a drone if the raycast hits a gameobject that has a Podium component and is not already occupied
                if (hitPodium != null && !hitPodium.IsOccupied)
                {
                    var drone = Instantiate(prefabToSpawn);
                    var droneSize = drone.transform.GetComponent<Renderer>().bounds.size.y;
                    
                    var podiumSize = PodiumDetector.ActivePodium.transform.GetComponent<Renderer>().bounds.size.y;
                    var podiumTop = podiumPos.y + podiumSize / 2;

                    // Positions the drone flat atop the active podium
                    drone.transform.position = new Vector3(podiumPos.x, podiumTop + droneSize/2, podiumPos.z);
                    drone.transform.SetParent(PodiumDetector.ActivePodium.transform); // BREAKS ATTACHMENT DETECTION??
                    
                    
                    hitPodium.SetPodiumDrone(drone.GetComponent<QuadcopterDrone>());
                    hitPodium.IsOccupied = true;
                }
                else
                {
                    Debug.Log("Podium already contains a drone");
                }
            }
        }
    }
}