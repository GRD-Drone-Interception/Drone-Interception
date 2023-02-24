using DroneLoadout;
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
    public BuildBudget BuildBudget { get; private set; }
    
    [SerializeField] private float startingBuildBudget = 10000;
    
    private void Awake()
    {
        DroneSwarm = new DroneSwarm();
        BuildBudget = new BuildBudget(startingBuildBudget);
    }
}