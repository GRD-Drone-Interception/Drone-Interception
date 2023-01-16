using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    UnitDeployment,
    Play,
    MatchAnalysis,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState gamesate;

    [SerializeField] private GameObject deploymentContainer;
    [SerializeField] private GameObject mapContainer;

    [SerializeField] private CameraRigController deploymentCameraRig;
    [SerializeField] private CameraRigController mapCameraRig;

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
        
    }

    void Update()
    {
        SetGameState(gamesate);

        switch (gamesate)
        {
            case GameState.UnitDeployment:
                mapContainer.SetActive(false);
                mapCameraRig.enabled = false;

                deploymentCameraRig.enabled = true;
                deploymentContainer.SetActive(true);
                break;
            case GameState.Play:
                deploymentContainer.SetActive(false);
                deploymentCameraRig.enabled = false;

                mapCameraRig.enabled = true;
                mapContainer.SetActive(true);
                break;
            case GameState.MatchAnalysis:
                deploymentContainer.SetActive(false);
                mapContainer.SetActive(false);


                break;
            default:
                break;
        }
    }

    public void SetGameState(GameState state)
    {
        gamesate = state;
        CinemachineCamManager.Instance.SetCameraState(state);
    }
}
