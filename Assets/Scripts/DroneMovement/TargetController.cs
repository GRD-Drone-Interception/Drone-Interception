using System;
using System.Collections.Generic;
using DroneMovement.Scripts;
using DroneMovement.Scripts.Boids;
using DroneSelection.Scripts;
using UnityEngine;

namespace DroneMovement
{
    public class TargetController : MonoBehaviour
    {
        public static TargetController Instance;
        public GameObject CurrentTarget => _currentTarget;

        [SerializeField] private WaypointSpawner waypointSpawner;
        private Queue<GameObject> _waypointTargets = new();
        private GameObject _currentTarget;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError($"There should be only one of {this} in the scene!");
            }
        }

        private void OnEnable() => waypointSpawner.OnWaypointPlaced += QueueWaypointTarget;
        private void OnDisable() => waypointSpawner.OnWaypointPlaced -= QueueWaypointTarget;

        public void Start()
        {
            //SetCurrentTarget(); // Set the initial target
        }

        public void SetCurrentTarget()
        {
            if (_waypointTargets.Count <= 0)
            {
                return;
            }
            _currentTarget = _waypointTargets.Peek(); // Get the current target object

            if (_currentTarget == null) // Check if the current target is null
            {
                // Get all objects with the "Defender" tag and destroy them
                GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");
                foreach (GameObject defender in defenders)
                {
                    Destroy(defender);
                }

                // Get all objects with the "Attacker" tag and set their velocity to zero
                GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");
                foreach (GameObject attacker in attackers)
                {
                    Boid boidScript = attacker.GetComponent<Boid>();
                    if (boidScript != null)
                    {
                        boidScript.enabled = false;
                    }
                    FixedWingAircraft fixedWingScript = attacker.GetComponent<FixedWingAircraft>();
                    if (fixedWingScript != null)
                    {
                        fixedWingScript.enabled = false;
                    }
                    scr_Drone droneScript = attacker.GetComponent<scr_Drone>();
                    if (droneScript != null)
                    {
                        droneScript.enabled = false;
                    }

                    Rigidbody attackerRb = attacker.GetComponent<Rigidbody>();
                    if (attackerRb != null)
                    {
                        attackerRb.velocity = Vector3.zero;
                        attackerRb.useGravity = false;
                    }
                }
            }
        }

        private void QueueWaypointTarget(GameObject nextTarget)
        {
            _waypointTargets.Enqueue(nextTarget);

            if (_waypointTargets.Count == 1)
            {
                _currentTarget = _waypointTargets.Peek();
            }
        }
        
        public void DequeueWaypointTarget()
        {
            _waypointTargets.Dequeue();
        }
    }
}
