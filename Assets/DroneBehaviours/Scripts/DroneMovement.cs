using System.Collections.Generic;
using DroneLoadout;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    [CreateAssetMenu(fileName = "NewDroneMovementBehaviour", menuName = "Drone/Behaviours/Movement")]
    public class DroneMovement : DroneBehaviour
    {
        //public float speed = 5f;
        //public float turnRate = 5f;
        //public float acceleration = 5f;
        //public float speedScaleFactor = 10f;
        private Vector3 _targetPos;
        private const float GameSpeed = 1.0f;
        private float _currentSpeed;
        private Drone _closestDrone;

        public override void UpdateBehaviour(Drone drone)
        {
            Debug.Log("Movement behaviour", drone);
            //Drone closestDrone = FindClosestDrone(DroneManager.ActiveDrones.Where(ctx => ctx.GetTeam() != drone.GetTeam()), drone);
            /*_closestDrone = FindClosestDrone(DroneManager.ActiveDrones, drone);
            if (_closestDrone != null)
            {
                _targetPos = _closestDrone.transform.position;
            }*/
        }

        public override void FixedUpdateBehaviour(Drone drone)
        { 
            /*if (_closestDrone != null)
            {
                drone.transform.position = Vector3.MoveTowards(drone.transform.position, _targetPos, drone.DroneConfigData.TopSpeed / 100 * Time.fixedDeltaTime);
                drone.transform.LookAt(_targetPos);
            }*/




            /*// Calculate the maximum speed based on the weight and top speed
            float maxSpeed = Mathf.Min((drone.DroneConfigData.Weight / 10.0f) + 10.0f, drone.DroneConfigData.TopSpeed * 1.609f); // in kilometers per hour

            // Calculate the maximum acceleration based on the weight
            float maxAcceleration = drone.DroneConfigData.Acceleration / 3.6f; // in meters per second squared

            // Calculate the force to apply to the drone based on its current speed and the maximum acceleration
            float force = (maxAcceleration * drone.Rb.mass) / Mathf.Clamp(_currentSpeed, 0.1f, maxSpeed);

            // Apply the force to the drone in the direction it's facing
            drone.Rb.AddForce(drone.transform.forward * force);

            // Calculate the current speed of the drone in miles per hour
            _currentSpeed = drone.Rb.velocity.magnitude * 2.237f; // in miles per hour

            // Clamp the speed of the drone based on its maximum speed
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0.0f, maxSpeed * 0.621f); // in miles per hour*/





            // Calculate the maximum speed and acceleration based on the weight
            /*float maxSpeed = (drone.DroneConfigData.Weight / 10.0f) + 10.0f; // in miles per hour
            float maxAcceleration = drone.DroneConfigData.Acceleration / 3.6f; // in meters per second squared

            // Calculate the force to apply to the drone based on its current speed and the maximum acceleration
            float force = (maxAcceleration * rb.mass) / Mathf.Clamp(currentSpeed, 0.1f, maxSpeed);

            // Apply the force to the drone in the direction it's facing
            rb.AddForce(transform.forward * force);

            // Clamp the speed of the drone based on its maximum speed
            currentSpeed = Mathf.Clamp(rb.velocity.magnitude * 2.237f, 0.0f, maxSpeed);*/



        

            // Real world speed in mph
            /*float realWorldSpeed = drone.DroneConfigData.TopSpeed/2f;
        
            // Calculate the scaled speed for gameplay by converting mph to mps (the unit used by Unity's physic engine)
            float scaledSpeed = realWorldSpeed * GameSpeed;
            
            drone.Rb.AddRelativeForce(Vector3.forward * (scaledSpeed * 0.44704f)); // Convert mph to meters per second*/
        
        
        
        

            // Move the drone forward at the scaled speed
            //Vector3 force = Vector3.forward * (scaledSpeed/* * drone.Rb.mass*/);
            //drone.Rb.AddRelativeForce(force);


        
        

            //drone.transform.Translate(Vector3.forward * (drone.DroneConfigData.TopSpeed / 3600f * Time.fixedDeltaTime));
            //drone.Rb.AddRelativeForce(Vector3.forward * (drone.DroneConfigData.TopSpeed * 0.44704f)); // Convert mph to meters per second
        }

        private Drone FindClosestDrone(IEnumerable<Drone> drones, Drone currentDrone)
        {
            Drone closestDrone = null;
            var closestDistance = float.MaxValue;

            foreach (Drone drone in drones)
            {
                if (drone == currentDrone)
                {
                    continue;
                }

                var distance = Vector3.Distance(currentDrone.transform.position, drone.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDrone = drone;
                }
            }
            return closestDrone;
        }
    }
}
