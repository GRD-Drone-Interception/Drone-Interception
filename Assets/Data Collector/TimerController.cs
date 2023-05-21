using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text timerText;

    public bool isRunning = false;
    private float currentTime;

    private void Start()
    {
        currentTime = 0f;
    }

    private void Update()
    {
        if (isRunning)
        {
            // Update the timer
            currentTime += Time.deltaTime;
            DisplayTime();

            // Check if there are no objects tagged "attacker" or "defender"
            if (GameObject.FindGameObjectsWithTag("Attacker").Length == 0 || GameObject.FindGameObjectsWithTag("Defender").Length == 0)
            {
                isRunning = false;
                Debug.Log("Game Over");
                // Perform any necessary actions when the timer stops
            }
        }
    }

    private void DisplayTime()
    {
        // Format the time as minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the text box with the current time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
