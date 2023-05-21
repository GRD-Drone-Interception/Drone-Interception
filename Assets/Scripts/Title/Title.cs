using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Image splashImage; // reference to the Image component of the splash screen
    public float fadeInTime = 1f; // time to fade in the splash image
    public float fadeOutTime = 1f; // time to fade out the splash image
    public float holdTime = 1f; // time to hold the splash image after fading out

    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(0f); // set the initial alpha value to 0

        // fade in the splash image
        splashImage.CrossFadeAlpha(1f, fadeInTime, false);

        // wait for the fade in to complete
        yield return new WaitForSeconds(fadeInTime);

        // hold the image for the specified hold time
        yield return new WaitForSeconds(holdTime);

        // fade out the splash image
        splashImage.CrossFadeAlpha(0f, fadeOutTime, false);

        // wait for the fade out to complete
        yield return new WaitForSeconds(fadeOutTime);

        // transition to the DroneWorkshop scene
        SceneManager.LoadScene("DroneWorkshop");
        Debug.Log("Transitioning to DroneWorkshop scene...");
    }
}
