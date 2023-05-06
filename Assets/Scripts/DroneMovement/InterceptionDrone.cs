using UnityEngine;

public class InterceptionDrone : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float acceleration = 10f;

    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        // Set the target to null initially
        target = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the "Enemy" tag
        if (other.CompareTag("Attacker"))
        {
            // Set the target to the other collider's transform
            target = other.transform;
            Debug.Log("Attacker detected!");
        }
    }

    private void Update()
    {
        // Check if there is a target
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Check if the target is within range
            if (direction.magnitude < range)
            {
                // Calculate the rotation to face the target
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate towards the target using turn speed and acceleration
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                currentVelocity = Vector3.Lerp(currentVelocity, direction.normalized * speed, acceleration * Time.deltaTime);

                // Move towards the target with the current velocity
                transform.Translate(currentVelocity * Time.deltaTime, Space.World);

                Debug.Log("Moving towards attacker!");
            }
            else
            {
                // Reset the target to null
                target = null;
            }
        }
    }
}
