using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelection : MonoBehaviour
{
    public Dropdown levelDropdown;

    private List<string> levelNames = new List<string> { "Castle Park (Bristol)", "Fleet Air Arm" };

    void Start()
    {
        levelDropdown.ClearOptions();
        levelDropdown.AddOptions(levelNames);
    }

    public void LoadLevel()
    {
        string selectedLevel = levelNames[levelDropdown.value];
        SceneManager.LoadScene(selectedLevel);
    }
}
