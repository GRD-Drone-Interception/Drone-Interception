using Core;
using DroneWorkshop;
using UnityEngine;

namespace BudgettingSystem
{
    public class BudgetCalculator : MonoBehaviour
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
            }
            else
            {
                Debug.Log("You cannot afford this purchase!");
            }
        }
        
        private void DepositToBudgetOnDroneSold(float amount)
        {
            player.BuildBudget.Deposit(amount);
        }
    }
}