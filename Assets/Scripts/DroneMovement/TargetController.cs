using System;
using UnityEngine;
using System.Collections.Generic;
using DroneMovement.Scripts;
using DroneMovement.Scripts.Boids;

public class TargetController : MonoBehaviour
{
    public static TargetController Instance;

    public List<GameObject> targets; // A list of all the target objects
    public int currentTargetIndex = 0; // The current target index
    public GameObject CurrentTarget; // The current target object

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

    public void Start()
    {
        SetCurrentTarget(); // Set the initial target
    }

    public void SetCurrentTarget()
    {
        CurrentTarget = targets[currentTargetIndex]; // Get the current target object

        if (CurrentTarget == null) // Check if the current target is null
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
}
