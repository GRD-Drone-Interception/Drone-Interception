using Drones;
using UnityEngine;

public enum PlayerTeam 
{
    Offensive, 
    Defensive 
}

public class Player : MonoBehaviour
{
    public PlayerTeam Team { get; set; }
    public DroneSwarm DroneSwarm { get; set; }

    private void Awake() => DroneSwarm = new DroneSwarm();
}