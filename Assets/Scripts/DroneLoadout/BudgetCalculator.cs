using System;
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

        private void OnEnable()
        {
            _workbench.OnDroneAdded += SubscribeToNewDronesDecoratedEvents;
            _workbench.OnDroneRemoved += UnsubscribeFromCurrentDronesDecoratedEvents;
        }

        private void OnDisable()
        {
            _workbench.OnDroneAdded -= SubscribeToNewDronesDecoratedEvents;
            _workbench.OnDroneRemoved -= UnsubscribeFromCurrentDronesDecoratedEvents;
        }

        private void SubscribeToNewDronesDecoratedEvents(Drone drone)
        {
            _player.BuildBudget.Spend(drone.DecorableDrone.Cost);
            _workbench.DroneOnBench.OnDroneDecorationAdded += (drone1, attachment) => _player.BuildBudget.Spend(attachment.Data.Cost);
            _workbench.DroneOnBench.OnDroneDecorationRemoved += (drone1, attachment) => _player.BuildBudget.Deposit(attachment.Data.Cost); 
        }

        private void UnsubscribeFromCurrentDronesDecoratedEvents(Drone drone)
        {
            _player.BuildBudget.Deposit(drone.DecorableDrone.Cost);
            _workbench.DroneOnBench.OnDroneDecorationAdded -= (drone1, attachment) => _player.BuildBudget.Spend(attachment.Data.Cost);
            _workbench.DroneOnBench.OnDroneDecorationRemoved -= (drone1, attachment) => _player.BuildBudget.Deposit(attachment.Data.Cost); 
        }
    }
}