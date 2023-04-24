using UnityEngine;
using UnityEngine.UI;

public class LevelImage : MonoBehaviour
{
    public Dropdown levelDropdown;
    public Image levelImage;

    public Sprite castleParkSprite;
    public Sprite fleetAirArmSprite;
    // add additional Sprites for each level in the Dropdown

    public Sprite defaultSprite;

    void Update()
    {
        switch (levelDropdown.value)
        {
            case 0:
                levelImage.sprite = castleParkSprite;
                break;
            case 1:
                levelImage.sprite = fleetAirArmSprite;
                break;
            // add additional cases for each level in the Dropdown
            default:
                levelImage.sprite = defaultSprite;
                break;
        }
    }
}
