using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button _quitButton;

    private void Awake() => _quitButton = GetComponent<Button>();
    private void OnEnable() => _quitButton.onClick.AddListener(QuitGame);
    private void OnDisable() => _quitButton.onClick.RemoveListener(QuitGame);

    private void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}