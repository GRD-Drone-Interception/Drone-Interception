using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelection : MonoBehaviour
{
    public Dropdown levelDropdown;

    private readonly List<string> _levelNames = new() { "Castle Park (Bristol)", "Fleet Air Arm" };

    void Start()
    {
        levelDropdown.ClearOptions();
        levelDropdown.AddOptions(_levelNames);
    }

    public void LoadLevel()
    {
        string selectedLevel = _levelNames[levelDropdown.value];
        SceneManager.LoadScene(selectedLevel);
    }
}