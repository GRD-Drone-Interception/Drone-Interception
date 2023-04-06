using Core;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    public PlayerTeam CurrentTeam { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"There is more than one {this} in the scene!");
        }
    }

    private void Start ()
    {
        CurrentTeam = PlayerTeam.Offensive; 
        StartTurn();               
    }

    private void StartTurn () 
    {
        
    }

    private void EndTurn ()
    {
        
    }
}

