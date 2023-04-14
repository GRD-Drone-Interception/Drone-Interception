using System;
using UnityEngine;
using System.Collections.Generic;

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
    }
}