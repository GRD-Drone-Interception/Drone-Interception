using UnityEngine;

public class InterceptionDrone : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;

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
                // Move towards the target
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
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
