using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    [CreateAssetMenu(fileName = "NewDroneMovementBehaviour", menuName = "Drone/Behaviours/Movement")]
    public class DroneMovement : DroneBehaviour
    {
        [SerializeField] private float thrust = 10f;
        [SerializeField] private float hoverHeight = 1f;
        [SerializeField] private float hoverForce = 10f;
        [SerializeField] private float maxSpeed = 10f;
        [SerializeField] private float maxForce = 20f;
        [SerializeField] private float slowingDistance = 15f;
        [SerializeField] private float rotationSpeed = 1.0f;
        private Transform _target;

        public override void UpdateBehaviour(Drone drone)
        {
            if (_target == null)
            {
                if (GameObject.Find("TargetCube") != null)
                {
                    _target = GameObject.Find("TargetCube").transform;
                }
                else
                {
                    _target = drone.transform;
                }
            }
        }

        public override void FixedUpdateBehaviour(Drone drone)
        {
            if (_target == null) { return; }

            // Calculate forces for thrust and hover
            float verticalVelocity = Mathf.Abs(drone.Rb.velocity.y);
            float verticalError = hoverHeight - drone.transform.position.y;
            float hoverForceMagnitude = Mathf.Clamp(verticalError * hoverForce, 0f, 1f) * thrust;
            Vector3 thrustForce = drone.transform.up * hoverForceMagnitude;

            // Calculate forces for movement towards target
            Vector3 targetDirection = (_target.position - drone.transform.position).normalized;
            float targetDistance = Vector3.Distance(drone.transform.position, _target.position);
            float speedFactor = Mathf.Clamp(targetDistance / maxSpeed, 0f, 1f);
            float slowingFactor = Mathf.Clamp01(targetDistance / slowingDistance);
            float targetSpeed = maxSpeed * slowingFactor;
            Vector3 targetVelocity = targetDirection * targetSpeed;
            Vector3 targetForce = (targetVelocity - drone.Rb.velocity) * maxForce;

            // Combine forces and apply to Rigidbody
            Vector3 totalForce = thrustForce + targetForce;
            drone.Rb.AddForce(totalForce, ForceMode.Force);
            
            // Rotate towards the target direction
            if (drone.Rb.velocity.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(drone.Rb.velocity.normalized, Vector3.up);
                drone.transform.rotation = Quaternion.Slerp(drone.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
