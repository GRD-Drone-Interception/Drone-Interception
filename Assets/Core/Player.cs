using BudgettingSystem;
using DroneLoadout.Scripts;
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
        public PlayerTeam Team { get; private set; }
        public DroneSwarm DroneSwarm { get; private set; }
        public BuildBudget BuildBudget { get; private set; }
    
        [SerializeField] private float startingBuildBudget = 10000;

        private void Awake()
        {
            DroneSwarm = new DroneSwarm();
            BuildBudget = new BuildBudget(startingBuildBudget);
        }

        private void Start()
        {
            //BudgetData budgetData = SaveSystem.Load("") as BudgetData;
            //BuildBudget.SetBudget(budgetData.budget);
        }
    }
}