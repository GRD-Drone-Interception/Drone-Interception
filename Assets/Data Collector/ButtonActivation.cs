using UnityEngine;
using UnityEngine.UI;

public class ButtonActivation : MonoBehaviour
{
    public Button button; // Reference to the button

    private bool canClick = false; // Flag to track whether the button can be clicked

    private void Start()
    {
        // Initially, disable the button
        button.interactable = false;

        // Check if there is at least one Attacker and one Defender in the scene
        GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");
        GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");

        if (attackers.Length > 0 && defenders.Length > 0)
        {
            canClick = true;
            button.interactable = true;
        }
    }

    private void Update()
    {
        // If the button is not clickable, check if there is now at least one Attacker and one Defender in the scene
        if (!canClick)
        {
            GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");
            GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");

            if (attackers.Length > 0 && defenders.Length > 0)
            {
                canClick = true;
                button.interactable = true;
            }
        }
    }
}
