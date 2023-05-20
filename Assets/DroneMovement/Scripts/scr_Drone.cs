using UnityEngine;

namespace DroneMovement.Scripts
{
    public class scr_Drone : MonoBehaviour
    {
        public float speed = 5f;
        public float rotationSpeed = 2f;
        public float maxVelocityChange = 10f;
        public float arrivalDistance = 1f;
        public Transform target;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            transform.rotation = Quaternion.identity;
        }

        private void FixedUpdate()
        {
            if (!BattleButton.Instance.Battle)
            {
                return;
            }
            
            if (target != null)
            {
                // Move towards the target
                Vector3 targetDirection = target.position - transform.position;
                targetDirection.Normalize();

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget > arrivalDistance)
                {
                    Vector3 velocity = rb.velocity;
                    Vector3 desiredVelocity = targetDirection * speed;
                    Vector3 steering = desiredVelocity - velocity;
                    steering = Vector3.ClampMagnitude(steering, maxVelocityChange);
                    rb.AddForce(steering, ForceMode.VelocityChange);

                    // Rotate towards the target
                    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                target = TargetController.Instance.CurrentTarget.transform;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger entered");
            if (other.CompareTag("Object"))
            {
                // If the object has collided with the target
                Destroy(TargetController.Instance.CurrentTarget); // Destroy the current target
                /*TargetController.Instance.currentTargetIndex++; // Increment the target index
                if (TargetController.Instance.currentTargetIndex >= TargetController.Instance.targets.Count)
                { // If we've reached the end of the targets list
                    TargetController.Instance.currentTargetIndex = 0; // Loop back to the start
                }*/
                TargetController.Instance.DequeueWaypointTarget();
                TargetController.Instance.SetCurrentTarget(); // Set the new current target
                target = TargetController.Instance.CurrentTarget.transform;
            }
        }
    }
}
