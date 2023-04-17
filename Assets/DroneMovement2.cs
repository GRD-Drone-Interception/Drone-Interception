using DroneLoadout.Scripts;
using UnityEngine;

public class DroneMovement2 : MonoBehaviour
{
    [SerializeField] private float thrust = 10f;
    [SerializeField] private float hoverHeight = 1f;
    [SerializeField] private float hoverForce = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float maxForce = 20f;
    [SerializeField] private float slowingDistance = 15f;
    [SerializeField] private float rotationSpeed = 1.0f;
    private Drone _drone;
    private Vector3 _targetPosition;
    private bool _hasTarget;

    private void Awake() => _drone = GetComponent<Drone>();

    private void FixedUpdate()
    {
        // Calculate forces for thrust and hover
        float verticalVelocity = Mathf.Abs(_drone.Rb.velocity.y);
        float verticalError = hoverHeight - _drone.transform.position.y;
        float hoverForceMagnitude = Mathf.Clamp(verticalError * hoverForce, 0f, 1f) * thrust;
        Vector3 thrustForce = _drone.transform.up * hoverForceMagnitude;

        if (_hasTarget)
        {
            // Calculate forces for movement towards target
            Vector3 targetDirection = (_targetPosition - _drone.transform.position).normalized;
            float targetDistance = Vector3.Distance(_drone.transform.position, _targetPosition);
            float speedFactor = Mathf.Clamp(targetDistance / maxSpeed, 0f, 1f);
            float slowingFactor = Mathf.Clamp01(targetDistance / slowingDistance);
            float targetSpeed = maxSpeed * slowingFactor;
            Vector3 targetVelocity = targetDirection * targetSpeed;
            Vector3 targetForce = (targetVelocity - _drone.Rb.velocity) * maxForce;
            Vector3 totalForce = thrustForce + targetForce;
            // Combine forces and apply to Rigidbody
            _drone.Rb.AddForce(totalForce, ForceMode.Force);
            
            // Rotate towards the target direction
            if (_drone.Rb.velocity.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_drone.Rb.velocity.normalized, Vector3.up);
                _drone.transform.rotation = Quaternion.Slerp(_drone.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            /*if (Vector3.Distance(_drone.transform.position, _targetPosition) > 3f)
            {
                _hasTarget = false;
            }*/
        }
        else
        {
            _drone.Rb.AddForce(thrustForce, ForceMode.Force);
        }
    }
    
    public void SetTarget(Vector3 targetPos)
    {
        _targetPosition = targetPos;
        _hasTarget = true;
    }
}
