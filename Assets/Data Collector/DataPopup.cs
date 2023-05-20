using UnityEngine;
using UnityEngine.UI;

public class DataPopup : MonoBehaviour
{
    private int attackerCountStart;
    private int defenderCountStart;

    private int attackerCountCurrent;
    private int defenderCountCurrent;

    private int attackerLosses;
    private int defenderLosses;

    public Text countText; // Reference to the UI text element
    public Text messageText; // Reference to the UI text element

    private bool isVisible;

    private BattleButton battleButton; // Reference to the BattleButton script

    void Start()
    {
        // Find the BattleButton script in the scene and get a reference to it
        battleButton = FindObjectOfType<BattleButton>();

        // Set the initial opacity of the UI text elements to 0
        Color textColor = countText.color;
        textColor.a = 0f;
        countText.color = textColor;

        textColor = messageText.color;
        textColor.a = 0f;
        messageText.color = textColor;

        isVisible = false;
    }

    void Update()
    {
        // Check if the battle has started
        if (battleButton != null && battleButton.Battle)
        {
            // Count the number of attackers and defenders in the scene
            GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");
            GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");
            attackerCountCurrent = attackers.Length;
            defenderCountCurrent = defenders.Length;

            // Update the losses based on the difference between starting and current counts
            if (attackerCountStart == 0)
                attackerLosses = 0; // Reset attacker losses if attackerCountStart is 0
            else
                attackerLosses = attackerCountStart - attackerCountCurrent;

            if (defenderCountStart == 0)
                defenderLosses = 0; // Reset defender losses if defenderCountStart is 0
            else
                defenderLosses = defenderCountStart - defenderCountCurrent;

            // Check if either count has reached 0, and turn on the opacity of the UI text elements if so
            if (attackerCountCurrent == 0 || defenderCountCurrent == 0)
            {
                if (!isVisible)
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

                    isVisible = true;
                }
            }

            // Update the UI text elements
            UpdateCountText();
        }
    }

    void UpdateCountText()
    {
        // Update the UI text element with the current counts and losses
        countText.text = "Attackers: " + attackerCountCurrent + " (Start: " + attackerCountStart + " Losses: " + attackerLosses + ")"
            + "\nDefenders: " + defenderCountCurrent + " (Start: " + defenderCountStart + " Losses: " + defenderLosses + ")";
    }
}