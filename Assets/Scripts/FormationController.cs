using UnityEngine;
using UnityEngine.UI;

public class FormationController : MonoBehaviour
{
    public Dropdown formationDropdown;

    private void Start()
    {
        // Add a listener to the dropdown so we can detect when the user changes the formation
        formationDropdown.onValueChanged.AddListener(delegate
        {
            OnFormationChanged(formationDropdown.value);
        });
    }

    private void OnFormationChanged(int value)
    {
        // Convert the dropdown value to a FormationType enum
        Boid.FormationType formation = (Boid.FormationType)value;

        // Find all objects with the "Attacker" tag and set their currentFormation field
        GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");
        foreach (GameObject attacker in attackers)
        {
            Boid boid = attacker.GetComponent<Boid>();
            if (boid != null)
            {
                boid.currentFormation = formation;
            }
        }
    }
}