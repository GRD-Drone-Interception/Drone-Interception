using DroneLoadout.DroneWorkbench;
using UnityEngine;

namespace DroneLoadout
{
    public class BudgetCalculator : MonoBehaviour
    {
        private Player _player;
        private Workbench _workbench;
        
        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _workbench = FindObjectOfType<Workbench>();
        }

        private void OnEnable() => _workbench.OnDroneOnBenchChanged += SubscribeToNewDronesDecoratedEvents;
        private void OnDisable() => _workbench.OnDroneOnBenchChanged -= SubscribeToNewDronesDecoratedEvents;
        
        private void SubscribeToNewDronesDecoratedEvents(Drone drone)
        {
            _workbench.DroneOnBench.OnDroneDecorationAdded += DecreaseBuildBudgetText;
            _workbench.DroneOnBench.OnDroneDecorationRemoved += IncreaseBuildBudgetText;
        }

        private void IncreaseBuildBudgetText(Drone drone, DroneAttachment droneAttachment)
        {
            _player.BuildBudget.Sell(droneAttachment.Data.Cost);
        }
        
        private void DecreaseBuildBudgetText(Drone drone, DroneAttachment droneAttachment)
        {
            _player.BuildBudget.Spend(droneAttachment.Data.Cost);
        }
    }
}