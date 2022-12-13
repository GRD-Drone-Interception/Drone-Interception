using System;
using UnityEngine;

namespace Drone
{
    public class Podium : MonoBehaviour
    {
        public bool IsOccupied { get; set; }
        //public bool IsActive { get; set; }

        [SerializeField] private QuadcopterDrone _quadcopterDrone;
        //[SerializeField] private IDrone _drone;

        /*public void SetPodiumDrone(IDrone drone)
        {
            _drone = drone;
        }*/
        
        public void SetPodiumDrone(QuadcopterDrone drone)
        {
            _quadcopterDrone = drone;
        }

        public void RemoveDroneFromPodium()
        {
            Destroy(_quadcopterDrone.gameObject);
            _quadcopterDrone = null;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.GetComponent<QuadcopterDrone>() != null)
            {
                Destroy(other.transform.GetComponent<Rigidbody>());
            }
        }
    }
}