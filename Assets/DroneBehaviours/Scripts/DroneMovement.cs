using DroneLoadout;
using DroneLoadout.Decorators;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    [CreateAssetMenu(fileName = "NewDroneMovementBehaviour", menuName = "Drone/Behaviours/Movement")]
    public class DroneMovement : DroneBehaviour
    {
        [SerializeField] private DroneConfigData droneConfigData;
        private Transform target;
        public float speed = 10f; // The speed of the drone
        public float rotationSpeed = 5f; // The rotation speed of the drone
        public float stoppingDistance = 5f; // The distance from the target at which the drone will stop

        public override void UpdateBehaviour(Drone drone)
        {
            //Debug.Log("Movement behaviour", drone);
            if (target == null)
            {
                //target = GameObject.Find("Cube").transform;
            }
        }

        public override void FixedUpdateBehaviour(Drone drone)
        { 
            if (target != null)
            {
                // Move towards the target
                Vector3 direction = (target.position - drone.transform.position).normalized;
                drone.Rb.MovePosition(drone.transform.position + direction * (speed * Time.fixedDeltaTime));

                // Rotate towards the target
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                drone.Rb.MoveRotation(Quaternion.Lerp(drone.transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            
                // Stop if close enough to the target
                /*if (Vector3.Distance(drone.transform.position, target.position) < stoppingDistance)
                {
                    drone.Rb.velocity = Vector3.zero;
                }*/
            }
        }
    }
}
