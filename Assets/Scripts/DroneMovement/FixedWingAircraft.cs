using UnityEngine;

public class FixedWingAircraft : MonoBehaviour
{
    public float speed = 50f;
    public float rotationSpeed = 2f;
    public float maxVelocityChange = 10f;
    public float arrivalDistance = 1f;
    public float rollSpeed = 5f;
    public float pitchSpeed = 5f;
    public float yawSpeed = 5f;
    public float maxRollAngle = 45f;
    public float maxPitchAngle = 30f;
    public float maxYawAngle = 45f;
    public float liftForce = 10f;
    public float maxAltitude = 500f;
    public float minAltitude = 10f;
    public float altitudeChangeSpeed = 5f;
    public Transform target;

    private Rigidbody rb;

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
                transform.LookAt(transform.position - targetDirection, Vector3.up);
            }
        }



        float roll = Input.GetAxis("Horizontal") * rollSpeed;
        float pitch = Input.GetAxis("Vertical") * pitchSpeed;
        float yaw = Input.GetAxis("Yaw") * yawSpeed;
        float altitude = Input.GetAxis("Altitude");

        // Roll
        Vector3 rollTorque = transform.forward * -roll;
        rb.AddTorque(rollTorque);

        // Pitch
        Vector3 pitchTorque = transform.right * pitch;
        rb.AddTorque(pitchTorque);

        // Yaw
        Vector3 yawTorque = transform.up * yaw;
        rb.AddTorque(yawTorque);

        // Calculate lift force based on altitude
        float altitudeRatio = Mathf.Clamp01((transform.position.y - minAltitude) / (maxAltitude - minAltitude));
        float calculatedLiftForce = liftForce * altitudeRatio;
        Vector3 liftForceVector = transform.up * calculatedLiftForce;
        rb.AddForce(liftForceVector);

        // Change altitude
        float altitudeVelocity = altitude * altitudeChangeSpeed;
        Vector3 altitudeForce = transform.up * altitudeVelocity;
        rb.AddForce(altitudeForce);

        // Clamp roll angle
        float rollAngle = transform.eulerAngles.z;
        if (rollAngle > 180f)
        {
            rollAngle -= 360f;
        }
        rollAngle = Mathf.Clamp(rollAngle, -maxRollAngle, maxRollAngle);

        // Clamp pitch angle
        float pitchAngle = transform.eulerAngles.x;
        if (pitchAngle > 180f)
        {
            pitchAngle -= 360f;
        }
        pitchAngle = Mathf.Clamp(pitchAngle, -maxPitchAngle, maxPitchAngle);

        // Clamp yaw angle
        float yawAngle = transform.eulerAngles.y;
        if (yawAngle > 180f)
        {
            yawAngle -= 360f;
        }
        yawAngle = Mathf.Clamp(yawAngle, -maxYawAngle, maxYawAngle);

        // Apply rotations
        transform.eulerAngles = new Vector3(pitchAngle, yawAngle, rollAngle);

        // Move forward
        Vector3 forwardVelocity = transform.forward * speed;
        rb.AddForce(forwardVelocity);
    }
}
