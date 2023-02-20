using UnityEngine;

public class scr_Drone : MonoBehaviour
{
    public float speed = 20f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the drone forward
        rb.AddRelativeForce(Vector3.forward * speed);

        // Rotate the drone left or right based on arrow key input
        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
