namespace DroneLoadout
{
    public class BuildBudget
    {
        public float StartBudget { get; }
        public float BudgetRemaining { get; private set; }

        public BuildBudget(float startingBudget)
        {
            StartBudget = startingBudget;
            BudgetRemaining = StartBudget;
        }

        public void SetBudget(float amount)
        {
            BudgetRemaining = amount;
        }

        public void Spend(float cost)
        {
            BudgetRemaining -= cost;
        }
        
        public void Deposit(float amount)
        {
            BudgetRemaining += amount;
        }
    
        public bool CanAfford(float cost)
        {
            return cost <= BudgetRemaining;
        }
    }
}
