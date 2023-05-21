using System.Collections.Generic;
using UnityEngine;

namespace DroneMovement.Scripts
{
    public class Interceptor : MonoBehaviour
    {
        public float speed = 10f;
        public float rotationSpeed = 5f;
        public Transform target;
        public Rigidbody rb;

        private static List<Transform> assignedTargets = new List<Transform>();

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                rb.rotation = Quaternion.Lerp(rb.rotation, rotation, rotationSpeed * Time.deltaTime);

                rb.velocity = transform.forward * speed;
            }
        }

        void OnCollisionEnter(Collision other)
        {
            FixedWingAircraft aircraft = other.gameObject.GetComponent<FixedWingAircraft>();
            if (aircraft != null)
            {
                aircraft.speed = 0f;
                aircraft.currentTarget = null;
            }

            scr_Drone copter = other.gameObject.GetComponent<scr_Drone>();
            if (copter != null)
            {
                copter.speed = 0f;
                copter.target = null;
            }

            rb.useGravity = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (target == null)
            {
                if (other.CompareTag("InterceptionTarget"))
                {
                    Transform newTarget = other.transform;
                    if (!assignedTargets.Contains(newTarget))
                    {
                        target = newTarget;
                        assignedTargets.Add(target);
                    }
                }
            }
        }
    }
}
