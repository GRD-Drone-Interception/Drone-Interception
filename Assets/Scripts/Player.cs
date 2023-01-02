using Drone;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Team Team { get; set; }
    public DroneSwarm DroneSwarm { get; set; }

    private void Awake() => DroneSwarm = new DroneSwarm();
}

public enum Team 
{
    Offensive, 
    Defensive 
}