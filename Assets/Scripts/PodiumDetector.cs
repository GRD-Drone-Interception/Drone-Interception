using System;
using Drone;
using UnityEngine;

public class PodiumDetector : MonoBehaviour
{
    public static Podium ActivePodium { get; private set; }
    
    private void OnTriggerEnter(Collider other)
    { 
        if (other.GetComponent<Podium>() != null)
        {
            ActivePodium = other.GetComponent<Podium>();
            Debug.Log($"Active Podium: {ActivePodium.name}");
        }
    }
}
