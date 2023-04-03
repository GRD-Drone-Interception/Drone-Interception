using UnityEngine;

public class scr_Drone : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 2f;
    public float maxVelocityChange = 10f;
    public float arrivalDistance = 1f;
    public float separationDistance = 2f; // New variable for flocking behavior
    public Transform target;

    private Rigidbody rb;
    private Collider[] nearbyColliders; // New array for detecting nearby objects
    private scr_Drone[] nearbyDrones; // New array for storing nearby drone scripts

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
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
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        // Flocking behavior
        nearbyColliders = Physics.OverlapSphere(transform.position, separationDistance); // Detect nearby colliders
        nearbyDrones = new scr_Drone[nearbyColliders.Length]; // Initialize nearby drone scripts array

        for (int i = 0; i < nearbyColliders.Length; i++)
        {
            if (nearbyColliders[i].gameObject != gameObject) // If not self
            {
                nearbyDrones[i] = nearbyColliders[i].gameObject.GetComponent<scr_Drone>(); // Get nearby drone script
            }
        }

        Vector3 flockDirection = Vector3.zero;
        int flockCount = 0;

        foreach (scr_Drone nearbyDrone in nearbyDrones)
        {
            if (nearbyDrone != null)
            {
                float distanceToNearbyDrone = Vector3.Distance(transform.position, nearbyDrone.transform.position);

                if (distanceToNearbyDrone < separationDistance)
                {
                    Vector3 separationDirection = transform.position - nearbyDrone.transform.position;
                    flockDirection += separationDirection.normalized;
                    flockCount++;
                }
            }
        }

        if (flockCount > 0)
        {
            flockDirection /= flockCount;
        }

        // Move with flock direction
        if (flockDirection != Vector3.zero)
        {
            Vector3 velocity = rb.velocity;
            Vector3 desiredVelocity = flockDirection.normalized * speed;
            Vector3 steering = desiredVelocity - velocity;
            steering = Vector3.ClampMagnitude(steering, maxVelocityChange);
            rb.AddForce(steering, ForceMode.VelocityChange);

            // Rotate towards flock direction
            float angle = Mathf.Atan2(flockDirection.y, flockDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
