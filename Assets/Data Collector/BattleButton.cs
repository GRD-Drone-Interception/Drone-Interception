using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    public static BattleButton Instance;
    
    public bool Battle = false;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

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
        _button.onClick.AddListener(ChangeBattleState);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeBattleState);
    }

    private void ChangeBattleState()
    {
        Battle = true;
        // Add your battle logic here
        TimerController timerController = FindObjectOfType<TimerController>();
        if (timerController != null)
        {
            timerController.isRunning = true;
        }
        FindObjectOfType<CameraFollow>().FindAndAddAllAttackerDrones();
    }
}
