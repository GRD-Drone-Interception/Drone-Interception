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

    private BattleButton battleButton; // Reference to the BattleButton script

    private bool isCounting; // Flag to indicate whether counting has started

    void Start()
    {
        // Find the BattleButton script in the scene and get a reference to it
        battleButton = GameObject.FindObjectOfType<BattleButton>();

        // Set the initial opacity of the UI text element to 1
        Color textColor = countText.color;
        textColor.a = 1f;
        countText.color = textColor;

        // Initialize the remaining counts and losses to the starting counts
        attackerCountStart = 0;
        defenderCountStart = 0;

        // Update the UI text element
        UpdateCountText();
    }

    void Update()
    {
        // Check if the battle has started
        if (battleButton != null && battleButton.battle)
        {
            if (!isCounting)
            {
                // Count the number of attackers and defenders at the beginning of the scene
                GameObject[] startAttackers = GameObject.FindGameObjectsWithTag("Attacker");
                GameObject[] startDefenders = GameObject.FindGameObjectsWithTag("Defender");
                attackerCountStart = startAttackers.Length;
                defenderCountStart = startDefenders.Length;

                isCounting = true; // Start counting
            }

            // Count the number of attackers and defenders in the scene
            GameObject[] currentAttackers = GameObject.FindGameObjectsWithTag("Attacker");
            GameObject[] currentDefenders = GameObject.FindGameObjectsWithTag("Defender");
            attackerCountCurrent = currentAttackers.Length;
            defenderCountCurrent = currentDefenders.Length;

            // Update the losses based on the difference between starting and current counts
            attackerLosses = attackerCountStart - attackerCountCurrent;
            defenderLosses = defenderCountStart - defenderCountCurrent;

            // Check if either count has reached 0, and update the opacity of the UI text element if so
            if (attackerCountCurrent == 0 || defenderCountCurrent == 0)
            {
                Color textColor = countText.color;
                textColor.a = 0f;
                countText.color = textColor;
                foreach (var defender in currentDefenders)
                {
                    defender.GetComponent<InterceptionDrone>().Target = null;
                }
            }

            // Check if the attackers or defenders have won, and display a message to the player if so
            if (attackerCountCurrent == 0)
            {
                Debug.Log("Defenders win!");
                // Set the speed of all Defender tagged objects to 0 and fix their positions
                foreach (GameObject defender in currentDefenders)
                {
                    defender.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    defender.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                // Display a message to the player using a UI element or other method
            }
            else if (defenderCountCurrent == 0)
            {
                Debug.Log("Attackers win!");
                // Display a message to the player using a UI element or other method
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
