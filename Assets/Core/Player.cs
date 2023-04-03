using DroneLoadout;
using DroneLoadout.Budgeting;
using UnityEngine;

namespace Core
{
    public enum PlayerTeam 
    {
        Offensive, 
        Defensive 
    }

    public class Player : MonoBehaviour
    {
        public PlayerTeam Team { get; set; }
        public DroneSwarm DroneSwarm { get; set; }
        //public Inventory Inventory { get; private set; }
        public BuildBudget BuildBudget { get; private set; }
    
        [SerializeField] private float startingBuildBudget = 10000;
        //[SerializeField] private int maxInventoryCapacity = 10;
    
        private void Awake()
        {
            DroneSwarm = new DroneSwarm();
            BuildBudget = new BuildBudget(startingBuildBudget);
            //Inventory = new Inventory(maxInventoryCapacity);
        }
    }
}