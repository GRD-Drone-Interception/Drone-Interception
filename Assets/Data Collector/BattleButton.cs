using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    public static BattleButton Instance;
    
    public bool HasBattleStarted = false;
    [SerializeField] private Button button;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"There should only be one of {this} at any one time");
        }
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ChangeBattleState);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ChangeBattleState);
    }

    private void ChangeBattleState()
    {
        HasBattleStarted = true;
        // Add your battle logic here
        TimerController timerController = FindObjectOfType<TimerController>();
        if (timerController != null)
        {
            timerController.isRunning = true;
        }
        FindObjectOfType<CameraFollow>().FindAndAddAllAttackerDrones();
        button.gameObject.SetActive(false);
    }
}
