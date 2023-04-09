using Core;
using DroneWorkshop;
using UnityEngine;
using Utility;

namespace BudgettingSystem
{
    public class WorkbenchBudgetCalculator : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private DroneWorkbench droneWorkbench;

        private void OnEnable()
        {
            droneWorkbench.OnDronePurchased += SpendBudgetOnDronePurchased;
            droneWorkbench.OnDroneSold += DepositToBudgetOnDroneSold;
        }

        private void OnDisable()
        {
            droneWorkbench.OnDronePurchased -= SpendBudgetOnDronePurchased;
            droneWorkbench.OnDroneSold -= DepositToBudgetOnDroneSold;
        }

        private void SpendBudgetOnDronePurchased(float cost)
        {
            if (player.BuildBudget.CanAfford(cost))
            {
                player.BuildBudget.Spend(cost);
                UpdateBudgetData();
            }
            else
            {
                Debug.Log("You cannot afford this purchase!");
            }
        }
        
        private void DepositToBudgetOnDroneSold(float amount)
        {
            player.BuildBudget.Deposit(amount);
            UpdateBudgetData();
        }

        private void UpdateBudgetData()
        {
            // Assemble the budget data
            BudgetData budgetData = new BudgetData();
            budgetData.budget = player.BuildBudget.BudgetRemaining;
            
            // Write it to file
            JsonFileHandler.Save(budgetData, "BudgetData");
        }
    }
}