using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Drone
{
    public class LoadGameScene : MonoBehaviour
    {
        private Button _playButton;

        private void Awake() => _playButton = GetComponent<Button>();
        private void OnEnable() => _playButton.onClick.AddListener(OnPlayButtonPressed);
        private void OnDisable() => _playButton.onClick.RemoveListener(OnPlayButtonPressed);

        private void OnPlayButtonPressed()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
