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
        private Vector3 _targetPosition;

        public void SetTarget(Vector3 targetPos)
        {
            _targetPosition = targetPos;
        }

        public override void UpdateBehaviour(Drone drone) {}

        public override void FixedUpdateBehaviour(Drone drone)
        {
            // Calculate forces for thrust and hover
            float verticalVelocity = Mathf.Abs(drone.Rb.velocity.y);
            float verticalError = hoverHeight - drone.transform.position.y;
            float hoverForceMagnitude = Mathf.Clamp(verticalError * hoverForce, 0f, 1f) * thrust;
            Vector3 thrustForce = drone.transform.up * hoverForceMagnitude;
            
            // Calculate forces for movement towards target
            Vector3 targetDirection = (_targetPosition - drone.transform.position).normalized;
            float targetDistance = Vector3.Distance(drone.transform.position, _targetPosition);
            float speedFactor = Mathf.Clamp(targetDistance / maxSpeed, 0f, 1f);
            float slowingFactor = Mathf.Clamp01(targetDistance / slowingDistance);
            float targetSpeed = maxSpeed * slowingFactor;
            Vector3 targetVelocity = targetDirection * targetSpeed;
            Vector3 targetForce = (targetVelocity - drone.Rb.velocity) * maxForce;
            Vector3 totalForce = thrustForce + targetForce;
            
            // Combine forces and apply to Rigidbody
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
