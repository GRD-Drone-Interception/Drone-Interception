using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAY,
    MAP,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState gamesate;

    [SerializeField] private GameObject MapContainer;

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
            case GameState.PLAY:
                MapContainer.SetActive(false);
                break;
            case GameState.MAP:
                MapContainer.SetActive(true);
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
