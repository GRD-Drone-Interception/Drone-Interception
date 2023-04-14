using UnityEngine;
using System.Collections.Generic;

public class TargetController : MonoBehaviour
{

    public List<GameObject> targets; // A list of all the target objects
    public int currentTargetIndex = 0; // The current target index
    public GameObject CurrentTarget; // The current target object

    public void Start()
    {
        SetCurrentTarget(); // Set the initial target
    }

    public void SetCurrentTarget()
    {
        CurrentTarget = targets[currentTargetIndex]; // Get the current target object
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.CompareTag("Object"))
        { // If the object has collided with the target
            Destroy(CurrentTarget); // Destroy the current target
            currentTargetIndex++; // Increment the target index
            if (currentTargetIndex >= targets.Count)
            { // If we've reached the end of the targets list
                currentTargetIndex = 0; // Loop back to the start
            }
            SetCurrentTarget(); // Set the new current target
        }
    }
}