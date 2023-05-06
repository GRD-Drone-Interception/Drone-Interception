using UnityEngine;
using UnityEngine.UI;

public class LevelDescription : MonoBehaviour
{
    public Dropdown levelDropdown;
    public Text levelDescriptionText;

    public string castleParkDescription;
    public string fleetAirArmDescription;
    // add additional strings for each level in the Dropdown

    public string defaultDescription;

    void Update()
    {
        switch (levelDropdown.value)
        {
            case 0:
                levelDescriptionText.text = castleParkDescription;
                break;
            case 1:
                levelDescriptionText.text = fleetAirArmDescription;
                break;
            // add additional cases for each level in the Dropdown
            default:
                levelDescriptionText.text = defaultDescription;
                break;
        }
    }
}
