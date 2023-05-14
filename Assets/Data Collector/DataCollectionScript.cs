using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCollectionScript : MonoBehaviour
{
    private int attackerCountStart;
    private int defenderCountStart;

    private int attackerCountCurrent;
    private int defenderCountCurrent;

    private int attackerLosses;
    private int defenderLosses;

    public Text countText; // Reference to the UI text element

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

        // Set the initial opacity of the UI text element to 1
        Color textColor = countText.color;
        textColor.a = 1f;
        countText.color = textColor;

        // Update the UI text element
        UpdateCountText();
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

        // Check if either count has reached 0, and update the opacity of the UI text element if so
        if (attackerCountCurrent == 0 || defenderCountCurrent == 0)
        {
            Color textColor = countText.color;
            textColor.a = 0f;
            countText.color = textColor;
        }

        // Check if the attackers or defenders have won, and display a message to the player if so
        if (attackerCountCurrent == 0)
        {
            Debug.Log("Defenders win!");
            // Display a message to the player using a UI element or other method
        }
        else if (defenderCountCurrent == 0)
        {
            Debug.Log("Attackers win!");
            // Display a message to the player using a UI element or other method
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
