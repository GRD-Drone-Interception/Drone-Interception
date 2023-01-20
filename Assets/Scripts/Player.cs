using Drone;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerTeam Team { get; set; }
    public DroneSwarm DroneSwarm { get; set; }

    private void Awake() => DroneSwarm = new DroneSwarm();
}

public enum PlayerTeam 
{
    Null,
    Offensive, 
    Defensive 
}