using Core;
using DroneLoadout.DroneWorkbench;
using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneLoadout.Budgeting
{
    public class BudgetCalculator : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Workbench workbench;

        private void OnEnable()
        {
            workbench.OnDronePurchased += SpendBudgetOnDronePurchased;
            workbench.OnDroneSold += DepositToBudgetOnDroneSold;
        }

        private void OnDisable()
        {
            workbench.OnDronePurchased -= SpendBudgetOnDronePurchased;
            workbench.OnDroneSold -= DepositToBudgetOnDroneSold;
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