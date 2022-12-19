using UnityEngine;
using UnityEngine.EventSystems;

namespace Drone
{
    public class DroneCreator : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject prefabToSpawn;
        private Workbench _workbench; // Unnecessary dependency

        private void Awake() => _workbench = FindObjectOfType<Workbench>();

        public void OnPointerDown(PointerEventData eventData)
        {
            var carousel = _workbench.Carousel;
            if (carousel.IsMoving)
            {
                Debug.LogWarning("Carousel is in motion, drone cannot be spawned!");
                return;
            }

            var currentNode = carousel.PodiumNodes[carousel.CurrentPodiumNodeIndex];
            if (_workbench.DronesOnPodiumDict.ContainsValue(currentNode))
            {
                Debug.LogWarning("A drone already occupies this node!");
                return;
            }
            
            // Check if the drone spawn button has been called again whilst a drone is still falling to bench

            SpawnDrone();
        }

        private void SpawnDrone()
        {
            var drone = Instantiate(prefabToSpawn);
            drone.transform.position = _workbench.DroneSpawnpoint.position;
        }
    }
}