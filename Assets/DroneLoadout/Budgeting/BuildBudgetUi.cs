using Core;
using TMPro;
using UnityEngine;

namespace DroneLoadout.Budgeting
{
    public class BuildBudgetUi : MonoBehaviour
    {
        private TMP_Text _buildBudgetCounter;
        private Player _player;

        private void Awake()
        {
            _buildBudgetCounter = GetComponent<TMP_Text>();
            _player = FindObjectOfType<Player>();
        }

        private void Start()
        {
            _buildBudgetCounter.text = $"BUILD BUDGET: {_player.BuildBudget.StartBudget:C0}";
        }

        private void Update() 
        {
            // TODO: Call on an event
            _buildBudgetCounter.text = $"BUILD BUDGET: {_player.BuildBudget.BudgetRemaining:C0}";
        }
    }
}