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
            Debug.Log("On pointer down");
            
            var podiumPos = PodiumDetector.ActivePodium.transform.position;
            
            Debug.Log("Podium Detector: " + PodiumDetector.ActivePodium);
            
            if(Physics.Raycast(podiumPos + Vector3.up/2, Vector3.down, out RaycastHit hitInfo))
            {
                Debug.DrawLine(podiumPos + Vector3.up/2, podiumPos, Color.green, 5.0f, false);

                var hitPodium = hitInfo.transform.GetComponent<Podium>();
                
                // FIX: Added a IgnoreRaycast layer to Drone parent object and it's attachment points
                // ISSUE: Now the attachment points won't work as they rely on raycasting to attach components

                if (hitPodium.IsOccupied)
                {
                    Debug.Log("Podium is occupied, attempting to remove existing drone");
                    hitPodium.RemoveDroneFromPodium();
                    hitPodium.IsOccupied = false;
                }
                
                if (hitPodium != null)
                {
                    //var drone = Instantiate(prefabToSpawn);
                    //var droneSize = drone.transform.GetComponent<Collider>().bounds.size.y;

                    //var podiumSize = PodiumDetector.ActivePodium.transform.GetComponent<Collider>().bounds.size.y;
                    //var podiumTop = podiumPos.y + podiumSize / 2;

                    // Positions the drone flat atop the active podium
                    //drone.transform.position = new Vector3(podiumPos.x, podiumTop + droneSize/2, podiumPos.z);
                    var drone = Instantiate(prefabToSpawn, podiumPos + Vector3.up, Quaternion.identity);
                    drone.transform.SetParent(PodiumDetector.ActivePodium.transform); // BREAKS ATTACHMENT DETECTION??

                    hitPodium.SetPodiumDrone(drone.GetComponent<QuadcopterDrone>());
                    hitPodium.IsOccupied = true;
                }
            }
        }
    }
}