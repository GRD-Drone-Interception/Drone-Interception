using DroneMovement;
using UnityEngine;
using UnityEngine.UI;

public class FixedWingAircraft : MonoBehaviour
{
    public float speed = 50f;
    public float rotationSpeed = 20f;
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
    public float collisionForce = 100f;

    private Rigidbody rb;
    public GameObject currentTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //currentTarget = _targetController.CurrentTarget;
    }

    private void RotateTowardsTarget(Vector3 targetPosition)
    {
        // Get the direction to the target
        Vector3 targetDirection = (targetPosition - transform.position).normalized;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void FixedUpdate()
    {
        if (TargetController.Instance.CurrentTarget == null)
        {
            return;
        }

        if (!BattleButton.Instance.HasBattleStarted)
        {
            return;
        }
        
        currentTarget = TargetController.Instance.CurrentTarget;
        var currentTargetOffset = currentTarget.transform.position + Vector3.up*2;

        // Move towards the current target
        Vector3 targetDirection = currentTargetOffset - transform.position;
        targetDirection.Normalize();

        float distanceToTarget = Vector3.Distance(transform.position, currentTargetOffset);

        if (distanceToTarget <= arrivalDistance)
        {
            // Set the next target
            TargetController.Instance.DequeueWaypointTarget();
            TargetController.Instance.SetCurrentTarget();
            currentTarget = TargetController.Instance.CurrentTarget;
        }
        else
        {
            // Rotate towards the target
            RotateTowardsTarget(currentTargetOffset);

            // Add force towards the target
            Vector3 velocity = rb.velocity;
            Vector3 desiredVelocity = targetDirection * speed;
            Vector3 steering = desiredVelocity - velocity;
            steering = Vector3.ClampMagnitude(steering, maxVelocityChange);
            rb.AddForce(steering, ForceMode.VelocityChange);
        }

        // Control the aircraft's movement
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

        // Keep the aircraft within altitude limits
        float currentAltitude = transform.position.y;

        if (altitude > 0f && currentAltitude < maxAltitude)
        {
            Vector3 altitudeChange = Vector3.up * altitudeChangeSpeed;
            rb.AddForce(altitudeChange);
        }
        else if (altitude < 0f && currentAltitude > minAltitude)
        {
            Vector3 altitudeChange = Vector3.down * altitudeChangeSpeed;
            rb.AddForce(altitudeChange);
        }



        // Limit roll, pitch, and yaw angles
        float rollAngle = Mathf.DeltaAngle(0f, transform.eulerAngles.z);
        float pitchAngle = Mathf.DeltaAngle(0f, transform.eulerAngles.x);
        float yawAngle = Mathf.DeltaAngle(0f, transform.eulerAngles.y);

        float clampedRollAngle = Mathf.Clamp(rollAngle, -maxRollAngle, maxRollAngle);
        float clampedPitchAngle = Mathf.Clamp(pitchAngle, -maxPitchAngle, maxPitchAngle);
        float clampedYawAngle = Mathf.Clamp(yawAngle, -maxYawAngle, maxYawAngle);

        Quaternion targetRotation = Quaternion.Euler(-clampedPitchAngle, clampedYawAngle, -clampedRollAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            Destroy(TargetController.Instance.CurrentTarget);
            TargetController.Instance.DequeueWaypointTarget();
            TargetController.Instance.SetCurrentTarget();
        }
    }
}
