using UnityEngine;
using UnityEngine.UI;

namespace Data_Collector
{
    public class DataPopup : MonoBehaviour
    {
        private int attackerCountStart;
        private int defenderCountStart;

        private int attackerCountCurrent;
        private int defenderCountCurrent;

        private int attackerLosses;
        private int defenderLosses;

        public Text countText; // Reference to the UI text element
        public Text messageText; // Reference to the UI text element to display messages

        void Start()
        {
            // Count the number of attackers and defenders at the beginning of the scene
            GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");
            GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");
            attackerCountStart = attackers.Length;
            defenderCountStart = defenders.Length;

            // Initialize the remaining counts and losses to the starting counts
            attackerCountCurrent = attackerCountStart;
            defenderCountCurrent = defenderCountStart;
            attackerLosses = 0;
            defenderLosses = 0;

            // Set the initial opacity of the UI text element to 0
            Color textColor = countText.color;
            textColor.a = 0f;
            countText.color = textColor;

            // Set the initial opacity of the message text element to 0
            textColor = messageText.color;
            textColor.a = 0f;
            messageText.color = textColor;
        }

        void Update()
        {
            // Count the number of attackers and defenders in the scene
            GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");
            GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");
            attackerCountCurrent = attackers.Length;
            defenderCountCurrent = defenders.Length;

            // Update the losses based on the difference between starting and current counts
            attackerLosses = attackerCountStart - attackerCountCurrent;
            defenderLosses = defenderCountStart - defenderCountCurrent;

            // Check if either count has reached 0, and turn on the opacity of the UI text elements if so
            if (attackerCountCurrent == 0 || defenderCountCurrent == 0)
            {
                Color textColor = countText.color;
                textColor.a = 1f;
                countText.color = textColor;

                textColor = messageText.color;
                textColor.a = 1f;
                messageText.color = textColor;

                // Determine which team has won and display a message to the player
                if (attackerCountCurrent == 0)
                {
                    messageText.text = "Defenders Win!";
                }
                else
                {
                    messageText.text = "Attackers Win!";
                }
            }

            // Update the UI text element
            UpdateCountText();
        }

        void UpdateCountText()
        {
            // Update the UI text element with the current counts and losses
            countText.text = "Attackers: " + attackerCountCurrent + " (Start: " + attackerCountStart + " Losses: " + attackerLosses + ")"
                             + "\nDefenders: " + defenderCountCurrent + " (Start: " + defenderCountStart + " Losses: " + defenderLosses + ")";
        }
    }
}
