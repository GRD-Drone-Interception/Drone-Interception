using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Testing
{
    public class LoadScene : MonoBehaviour
    {
        private enum Scene
        {
            Workshop,
            Game
        }

        [SerializeField] private Scene scene;
        private Button _playButton;

        private void Awake() => _playButton = GetComponent<Button>();
        private void OnEnable() => _playButton.onClick.AddListener(OnPlayButtonPressed);
        private void OnDisable() => _playButton.onClick.RemoveListener(OnPlayButtonPressed);

        private void OnPlayButtonPressed()
        {
            switch (scene)
            {
                case Scene.Workshop:
                    SceneManager.LoadScene("DroneWorkshop");
                    break;
                case Scene.Game:
                    SceneManager.LoadScene("Scenario1");
                    break;
            }
        }
    }
}

