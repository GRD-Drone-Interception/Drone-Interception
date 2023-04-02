using UnityEngine;

namespace Core
{
    public enum GameState
    {
        UnitDeployment,
        Play,
        Map,
        MatchAnalysis,
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState gamesate;

        // Managers
        InputManager inputManager;
        UIManager    uiManager;
        CameraRigManager   camManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                Debug.LogWarning($"There should only be one instance of {Instance.GetType()}");
            }
        }

        void Start()
        {
            inputManager = InputManager.Instance;
            uiManager = UIManager.Instance;
            camManager = CameraRigManager.Instance;
        }

        void Update()
        {
            SetGameState(gamesate);
        }

        public void SetGameState(GameState state)
        {
            gamesate = state;

            switch (gamesate)
            {
                case GameState.UnitDeployment:
                    inputManager.SetActiveActionMap(inputManager.inputActions.UnitDeployment);
                    uiManager.SetActiveUI(UIManager.Instance.DeploymentUI);
                    break;

                case GameState.Play:
                    inputManager.SetActiveActionMap(inputManager.inputActions.Game);
                    uiManager.SetActiveUI(UIManager.Instance.GameUI);
                    camManager.EnableGameCam();
                    break;

                case GameState.Map:
                    inputManager.SetActiveActionMap(inputManager.inputActions.Map);
                    uiManager.SetActiveUI(UIManager.Instance.MapUI);
                    camManager.EnableMapCam();
                    break;

                case GameState.MatchAnalysis:

                    break;

                default:
                    break;
            }
        }
    }
}